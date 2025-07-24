using Base.Infrastructure;
using Base.Services.Factories.Game;
using Base.Soldier;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SoldierSelector
{
    private LayerMask _mask;
    private float _raycastLength = 200f;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly InputService _inputService;
    private readonly Image _selectionBox;
    private readonly Camera _camera;
    private readonly Team _team;
    private Vector3 _firstPosition;
    private Vector3 _secondPosition;

    private Coroutine _selectCoroutine;
    private Coroutine _drawSelectionBoxCoroutine;

    public SoldierSelector(RaycastSettings soldierSelectorSettings, ICoroutineRunner coroutineRunner, InputService inputService,
        Image selectionBorder, Team team)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(ControlPointSelector), soldierSelectorSettings);

        _mask = soldierSelectorSettings.LayerMask;
        _raycastLength = soldierSelectorSettings.RaycastLength;
        _coroutineRunner = coroutineRunner;
        _inputService = inputService;
        _selectionBox = selectionBorder;
        _camera = Camera.main;
        _team = team;
    }

    public event Action<List<SoldierModel>> SoldiersSelected;

    public void Enable()
    {
        _inputService.Game.Select.performed += OnSelectSoldier;
    }

    public void Disable()
    {
        _inputService.Game.Select.performed -= OnSelectSoldier;

        StopSelectionCoroutine();
        StopDrawSelectionBoxCoroutine();
    }

    public bool TrySelectSoldier(out SoldierModel soldier, TeamType team)
    {
        soldier = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //_coroutineRunner.LaunchCoroutine(SelectCoroutine());

        //Collider[] colliders = Physics.OverlapBox(Vector3.zero, new Vector3(100, 100, 100), Quaternion.identity, _mask);
        //Debug.Log($"soldier colliders found = {colliders.Length}");
        //string message = string.Empty;
        //foreach (Collider collider in colliders)
        //{
        //    message += Camera.main.WorldToScreenPoint(collider.transform.position);
        //    message += "\n";
        //}

        //Debug.Log($"soldiers screenpoint position:\n{message}");

        //if (Physics.Raycast(ray, out RaycastHit hit, _raycastLength, _mask))

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

    private void OnSelectSoldier(InputAction.CallbackContext context)
    {
        if (_selectCoroutine != null)
            StopSelectionCoroutine();

        _selectCoroutine = _coroutineRunner.LaunchCoroutine(SelectCoroutine());
    }

    private IEnumerator SelectCoroutine()
    {
        List<SoldierModel> selectedSoldiers = new();
        _firstPosition = Input.mousePosition;

        _drawSelectionBoxCoroutine = _coroutineRunner.LaunchCoroutine(DrawSelectionBoxCoroutine());

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

        _secondPosition = Input.mousePosition;

        if ((_secondPosition.x - _firstPosition.x) < 1f && (_secondPosition.x - _firstPosition.x) >= 0)
        {
            Debug.Log($"TO SMALL SELECTION BOX");
            if (TrySelectSoldier(out SoldierModel soldier, TeamType.Player))
            {
                SoldiersSelected?.Invoke(new List<SoldierModel> { soldier });
            }

            StopDrawSelectionBoxCoroutine();
            yield break;
        }

        Collider[] colliders = Physics.OverlapBox(Vector3.zero, new Vector3(100, 100, 100), Quaternion.identity, _mask);
        Debug.Log($"soldier colliders found = {colliders.Length}");

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out SoldierSetup setup))
            {
                SoldierModel soldier = setup.GetSoldier();

                Debug.Log($"Soldier = {setup.gameObject.name}");

                if (soldier.GetTeam() == _team.Type && IsSelectable(soldier))
                    if (IsPointInSelectionBox(soldier.GetTransform().position, _firstPosition, _secondPosition))
                        selectedSoldiers.Add(setup.GetSoldier());
            }
        }

        if (selectedSoldiers.Count > 0)
            SoldiersSelected?.Invoke(selectedSoldiers);

        StopDrawSelectionBoxCoroutine();
    }

    private IEnumerator DrawSelectionBoxCoroutine()
    {
        _selectionBox.enabled = true;
        _selectionBox.rectTransform.anchoredPosition = _firstPosition;
        Debug.Log($"FIRST POSITION = {_firstPosition}");

        while (true)
        {
            _secondPosition = Input.mousePosition;
            float width = _secondPosition.x - _firstPosition.x;

            if (width < 0)
                width = 1f;

            float height = _firstPosition.y - _secondPosition.y;

            if (height < 0)
                height = 1f;

            _selectionBox.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            _selectionBox.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            yield return null;
        }
    }

    private bool IsSelectable(SoldierModel soldier)
    {
        return soldier.IsDead() == false && soldier.IsAttacking() == false;
    }

    private bool IsPointInSelectionBox(Vector3 worldPosition, Vector3 leftUpPoint, Vector3 rightBottomPoint)
    {
        Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);

        return screenPosition.x > leftUpPoint.x && screenPosition.x < rightBottomPoint.x && screenPosition.y > rightBottomPoint.y && screenPosition.y < leftUpPoint.y;
    }

    private void StopDrawSelectionBoxCoroutine()
    {
        if (_drawSelectionBoxCoroutine != null)
        {
            _coroutineRunner.EndCoroutine(_drawSelectionBoxCoroutine);
            _drawSelectionBoxCoroutine = null;
            _selectionBox.enabled = false;
        }
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
