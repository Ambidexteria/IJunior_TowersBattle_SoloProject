using Base.Services.Input;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Team))]
public class SoldierCommandController : MonoBehaviour
{
    [SerializeField] private float _secondClickDelay = 0.1f;
    [SerializeField] private SoldierSelector _soldierSelector;
    [SerializeField] private ControlPointSelector _controlPointSelector;
    [SerializeField] private FloatingPointer _floatingPointer;

    private Team _team;
    private InputService _input;
    private Coroutine _coroutine;

    private WaitForSeconds _waitForSeconds;
    private WaitUntil _waitUntilNextClick;
    private bool _playerClickLeftMouseButton = false;

    //public event Action<Soldier> SoldierSelected;

    private void Awake()
    {
        _team = GetComponent<Team>();

        _waitForSeconds = new(_secondClickDelay);
        _waitUntilNextClick = new(() => _playerClickLeftMouseButton == true);
    }

    private void OnEnable()
    {
        _input.Game.Select.performed += OnSelect;
        _input.Game.Select.performed += ClickLeftMouseButton;
    }

    private void OnDisable()
    {
        _input.Game.Select.performed -= OnSelect;
        _input.Game.Select.performed -= ClickLeftMouseButton;
    }

    [Inject]
    private void Construct(InputService input/*, SoldierSelector soldierSelector*/)
    {
        _input = input;
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        _coroutine = StartCoroutine(TrySendSoldierToControlPoint());
        
    }

    private IEnumerator TrySendSoldierToControlPoint()
    {
        if (_soldierSelector.TrySelectSoldier(out Soldier soldier, _team.Type) == false)
        {
            yield break;
        }

        Debug.Log("soldier selected");

        _floatingPointer.PlaceAbove(soldier.transform);

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
            yield break;
        }

        _floatingPointer.Hide();
        _playerClickLeftMouseButton = false;
        _coroutine = null;

        Debug.Log("Method ended");
    }

    private void ClickLeftMouseButton(InputAction.CallbackContext context)
    {
        _playerClickLeftMouseButton = true;
    }
}
