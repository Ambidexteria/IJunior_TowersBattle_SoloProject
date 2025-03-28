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
    private PlayerInput _playerInput;
    private Coroutine _coroutine;

    private WaitForSeconds _waitForSeconds;
    private WaitUntil _waitUntilNextClick;
    private bool _playerClickLeftMouseButton = false;

    public event Action<Soldier> SoldierSelected;

    private void Awake()
    {
        _team = GetComponent<Team>();

        _waitForSeconds = new(_secondClickDelay);
        _waitUntilNextClick = new(() => _playerClickLeftMouseButton == true);
    }

    private void OnEnable()
    {
        _playerInput.Game.Select.performed += OnSelect;
        _playerInput.Game.Select.performed += ClickLeftMouseButton;
    }

    private void OnDisable()
    {
        _playerInput.Game.Select.performed -= OnSelect;
        _playerInput.Game.Select.performed -= ClickLeftMouseButton;
    }

    [Inject]
    private void Construct(PlayerInput playerInput/*, SoldierSelector soldierSelector*/)
    {
        _playerInput = playerInput;
        //_soldierSelector = soldierSelector;
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

        _floatingPointer.PlaceAbove(soldier.transform);

        yield return _waitForSeconds;

        _playerClickLeftMouseButton = false;

        yield return _waitUntilNextClick;

        if(_controlPointSelector.TrySelectControlPoint(out ControlPoint controlPoint))
        {
            soldier.MoveTo(controlPoint.transform);
        }
        else
        {
            Debug.Log("Control Point isn't selected");
            yield break;
        }

        _floatingPointer.Hide();
        _playerClickLeftMouseButton = false;
        _coroutine = null;
    }

    private void ClickLeftMouseButton(InputAction.CallbackContext context)
    {
        _playerClickLeftMouseButton = true;
    }
}
