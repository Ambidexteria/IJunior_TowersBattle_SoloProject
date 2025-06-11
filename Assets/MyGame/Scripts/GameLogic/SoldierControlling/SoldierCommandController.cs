using Base.Infrastructure;
using Base.Services.Input;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class SoldierCommandController
{
    private float _secondClickDelay = 0.1f;
    private SoldierSelector _soldierSelector;
    private ControlPointSelector _controlPointSelector;
    private FloatingPointer _floatingPointer;

    private ICoroutineRunner _coroutineRunner;
    private Team _team;
    private InputService _input;
    private Coroutine _coroutine;


    private WaitForSeconds _waitForSeconds;
    private WaitUntil _waitUntilNextClick;
    private bool _playerClickLeftMouseButton = false;

    public SoldierCommandController(float secondClickDelay, SoldierSelector soldierSelector,
        ControlPointSelector controlPointSelector, FloatingPointer floatingPointer,
        ICoroutineRunner coroutineRunner, Team team, InputService input)
    {
        _secondClickDelay = secondClickDelay;
        _soldierSelector = soldierSelector;
        _controlPointSelector = controlPointSelector;
        _floatingPointer = floatingPointer;
        _coroutineRunner = coroutineRunner;
        _team = team;
        _input = input;

        _waitForSeconds = new(_secondClickDelay);
        _waitUntilNextClick = new(() => _playerClickLeftMouseButton == true);
    }

    public void Enable()
    {
        if (_input != null)
        {
            _input.Game.Select.performed += OnSelect;
            _input.Game.Select.performed += ClickLeftMouseButton;
        }
    }

    public void Disable()
    {
        _input.Game.Select.performed -= OnSelect;
        _input.Game.Select.performed -= ClickLeftMouseButton;
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        if (_coroutine == null)
            _coroutine = _coroutineRunner.LaunchCoroutine(TrySendSoldierToControlPoint());
    }

    private IEnumerator TrySendSoldierToControlPoint()
    {
        if (_soldierSelector.TrySelectSoldier(out SoldierModel soldier, _team.Type) == false)
        {
            yield break;
        }

        Debug.Log("soldier selected");

        _floatingPointer.PlaceAbove(soldier.GetTransform());

        yield return _waitForSeconds;

        Debug.Log("Delay ended");

        _playerClickLeftMouseButton = false;

        yield return _waitUntilNextClick;

        Debug.Log("Next click registered");

        if (_controlPointSelector.TrySelectControlPoint(out ControlPoint controlPoint))
        {
            soldier.MoveTo(controlPoint.transform);
            Debug.Log("Soldier sended to point");
        }
        else
        {
            Debug.Log("Control Point isn't selected");
        }

        _floatingPointer.Hide();
        _playerClickLeftMouseButton = false;
        _coroutine = null;

        Debug.Log("Method ended");

        StopCoroutine();
    }

    private void StopCoroutine()
    {
        if(_coroutine != null)
        {
            _coroutineRunner.EndCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void ClickLeftMouseButton(InputAction.CallbackContext context)
    {
        _playerClickLeftMouseButton = true;
    }
}
