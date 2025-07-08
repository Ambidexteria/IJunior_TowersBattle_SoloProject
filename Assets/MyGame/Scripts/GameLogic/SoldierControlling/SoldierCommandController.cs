using Base.Infrastructure;
using Base.Services.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierCommandController
{
    private float _secondClickDelay = 0.1f;
    private SoldierSelector _soldierSelector;
    private ControlPointSelector _controlPointSelector;
    private FloatingPointer _floatingPointer;

    private ICoroutineRunner _coroutineRunner;
    private Team _team;
    private InputService _input;
    private readonly AudioPlayerService _audioPlayer;
    private Coroutine _coroutine;


    private WaitForSeconds _waitForSeconds;
    private WaitUntil _waitUntilNextClick;
    private bool _playerClickLeftMouseButton = false;

    public SoldierCommandController(float secondClickDelay, SoldierSelector soldierSelector,
        ControlPointSelector controlPointSelector, FloatingPointer floatingPointer,
        ICoroutineRunner coroutineRunner, Team team, InputService input, AudioPlayerService audioPlayer)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(SoldierCommandController), soldierSelector,
            controlPointSelector,  floatingPointer,coroutineRunner,  team,  input);

        _secondClickDelay = secondClickDelay;
        _soldierSelector = soldierSelector;
        _controlPointSelector = controlPointSelector;
        _floatingPointer = floatingPointer;
        _coroutineRunner = coroutineRunner;
        _team = team;
        _input = input;
        _audioPlayer = audioPlayer;
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
        ExceptionsTest.NullRefMethodTest(nameof(SoldierCommandController), nameof(context), context);

        if (_coroutine == null)
            _coroutine = _coroutineRunner.LaunchCoroutine(TrySendSoldierToControlPoint());
    }

    private IEnumerator TrySendSoldierToControlPoint()
    {
        if (_soldierSelector.TrySelectSoldier(out SoldierModel soldier, _team.Type) == false)
            yield break;

        _floatingPointer.PlaceAbove(soldier.GetTransform());
        _audioPlayer.PlaySoldierRandomAnswerSound();

        yield return _waitForSeconds;

        _playerClickLeftMouseButton = false;

        yield return _waitUntilNextClick;

        if (_controlPointSelector.TrySelectControlPoint(out ControlPoint controlPoint))
            soldier.MoveTo(controlPoint.transform);

        _floatingPointer.Hide();
        _playerClickLeftMouseButton = false;
        _coroutine = null;

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
        ExceptionsTest.NullRefMethodTest(nameof(SoldierCommandController), nameof(context), context);

        _playerClickLeftMouseButton = true;
    }
}
