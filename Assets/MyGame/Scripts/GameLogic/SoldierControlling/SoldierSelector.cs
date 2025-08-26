using System;
using System.Collections;
using System.Collections.Generic;
using Base.GameLogic;
using Base.Infrastructure;
using Base.Services.Audio;
using Base.Services.Factories.Game;
using Base.Soldier;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierSelector
{
    private readonly LayerMask _mask;
    private readonly float _raycastLength;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly InputService _inputService;
    private readonly SelectionBoxDrawer _selectionBoxDrawer;
    private readonly Team _team;
    private readonly AudioPlayerService _audioPlayer;
    private readonly List<SoldierModel> _selectedSoldiers;
    private readonly float _secondCLickDelay;
    private readonly Camera _camera;
    private readonly WaitUntil _waitForPlayerClick;

    private bool _enabled = false;

    private Vector3 _firstPosition;
    private Vector3 _secondPosition;

    private Coroutine _selectCoroutine;

    private bool _selectionActive;

    public SoldierSelector(
        RaycastSettings soldierSelectorSettings,
        ICoroutineRunner coroutineRunner,
        InputService inputService,
        SelectionBoxDrawer selectionBoxDrawer,
        Team team,
        AudioPlayerService audioPlayer,
        float secondCLickDelay = 0.2f)
    {
        _mask = soldierSelectorSettings.LayerMask;
        _raycastLength = soldierSelectorSettings.RaycastLength;
        _coroutineRunner = coroutineRunner;
        _inputService = inputService;
        _selectionBoxDrawer = selectionBoxDrawer;
        _team = team;
        _audioPlayer = audioPlayer;
        _selectedSoldiers = new List<SoldierModel>();
        _secondCLickDelay = secondCLickDelay;
        _camera = Camera.main;

        _waitForPlayerClick = new WaitUntil(() => Input.GetMouseButtonUp(0));
    }

    public event Action<List<SoldierModel>> SoldiersSelected;

    public void Enable()
    {
        if (_enabled)
            return;

        _enabled = true;

        _inputService.Game.Select.performed += OnSelectPerformed;
    }

    public void Disable()
    {
        if (_enabled == false)
            return;

        _enabled = false;

        _inputService.Game.Select.performed -= OnSelectPerformed;
        _selectionBoxDrawer.Stop();

        StopSelectionCoroutine();
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        if (_selectionActive)
            return;

        if (_selectCoroutine != null)
            _coroutineRunner.EndCoroutine(_selectCoroutine);

        _selectCoroutine = _coroutineRunner.LaunchCoroutine(LaunchSelectionHandlerCoroutine());
    }

    private IEnumerator LaunchSelectionHandlerCoroutine()
    {
        _selectedSoldiers.Clear();
        _firstPosition = Input.mousePosition;

        while (Input.GetMouseButtonUp(0) == false)
        {
            _secondPosition = Input.mousePosition;
            _selectionBoxDrawer.Draw(_firstPosition, _secondPosition);
            yield return null;
        }

        if (IsSelectionBoxTooSmall())
        {
            if (TrySelectSoldier(out SoldierModel soldier, TeamType.Player))
            {
                _selectedSoldiers.Add(soldier);
            }
        }
        else
        {
            SelectAllSoldiersOnStage();
        }

        _selectionBoxDrawer.Stop();

        if (_selectedSoldiers.Count == 0)
            yield break;

        SoldiersSelected?.Invoke(_selectedSoldiers);
        _audioPlayer.PlaySoldierRandomAnswerSound();
        _selectionActive = true;

        ShowSelectionOnSoldiers();

        yield return new WaitForSeconds(_secondCLickDelay);

        while (_selectionActive)
        {
            yield return _waitForPlayerClick;

            CastSingleRay();
        }
    }

    private void ShowSelectionOnSoldiers()
    {
        foreach (var soldier in _selectedSoldiers)
            soldier.ShowSelectionCircle();
    }

    private void SelectAllSoldiersOnStage()
    {
        Collider[] colliders = Physics.OverlapBox(Vector3.zero, new Vector3(100, 100, 100), Quaternion.identity, _mask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out SoldierSetup setup))
            {
                SoldierModel soldier = setup.GetSoldier();

                if (soldier.GetTeam() == _team.Type && IsSelectable(soldier))
                    if (IsPointInSelectionBox(soldier.GetTransform().position, _firstPosition, _secondPosition))
                        _selectedSoldiers.Add(setup.GetSoldier());
            }
        }
    }

    private bool IsSelectionBoxTooSmall()
    {
        return (_secondPosition.x - _firstPosition.x) < 1f && (_secondPosition.x - _firstPosition.x) >= 0;
    }

    private void CastSingleRay()
    {
        Transform targetPosition;
        SoldierModel soldier;

        if (TrySelect(out ISelectable selectable))
        {
            if (selectable is ControlPoint)
            {
                targetPosition = (selectable as ControlPoint).transform;

                foreach (var tempSoldier in _selectedSoldiers)
                    if (tempSoldier.IsAttacking() == false && tempSoldier.IsDead() == false)
                        tempSoldier.MoveTo(targetPosition);

                _selectionActive = false;
                DeselectSoldiers();
            }
            else if (selectable is SoldierSetup)
            {
                DeselectSoldiers();

                soldier = (selectable as SoldierSetup).GetSoldier();

                if (soldier.GetTeam() == _team.Type)
                {
                    _selectedSoldiers.Add(soldier);
                    soldier.ShowSelectionCircle();
                }
            }
        }
        else
        {
            DeselectSoldiers();
            _selectionActive = false;
        }
    }

    private void DeselectSoldiers()
    {
        foreach (var tempSoldier in _selectedSoldiers)
            tempSoldier.HideSelectionCircle();

        _selectedSoldiers.Clear();
    }

    private bool TrySelect(out ISelectable selectable)
    {
        selectable = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, _raycastLength, _mask);

        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent(out ControlPoint controlpoint))
            {
                selectable = controlpoint;
                return true;
            }
            else if (hit.transform.TryGetComponent(out SoldierSetup soldierSetup))
            {
                selectable = soldierSetup;
                return true;
            }
        }

        return false;
    }

    private bool TrySelectSoldier(out SoldierModel soldier, TeamType team)
    {
        soldier = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(ray, 1f, out RaycastHit hit, _raycastLength, _mask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.transform.root.TryGetComponent(out SoldierSetup setup))
            {
                if (setup.GetSoldier().GetTeam() == team && IsSelectable(setup.GetSoldier()))
                {
                    soldier = setup.GetSoldier();
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsSelectable(SoldierModel soldier)
    {
        return soldier.IsDead() == false && soldier.IsAttacking() == false;
    }

    private bool IsPointInSelectionBox(Vector3 worldPosition, Vector3 leftUpPoint, Vector3 rightBottomPoint)
    {
        Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);

        return screenPosition.x >= leftUpPoint.x && screenPosition.x <= rightBottomPoint.x && screenPosition.y >= rightBottomPoint.y && screenPosition.y <= leftUpPoint.y;
    }

    private void StopSelectionCoroutine()
    {
        if (_selectCoroutine != null)
        {
            _coroutineRunner.EndCoroutine(_selectCoroutine);
            _selectCoroutine = null;
        }
    }
}
