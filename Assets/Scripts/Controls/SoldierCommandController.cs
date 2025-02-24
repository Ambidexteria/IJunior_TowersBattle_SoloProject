using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class SoldierCommandController : MonoBehaviour
{
    [SerializeField] private float _secondClickDelay = 0.1f;
    [SerializeField] private SoldierSelector _soldierSelector;
    [SerializeField] private ControlPointSelector _controlPointSelector;

    private PlayerInput _playerInput;
    private Coroutine _coroutine;

    private WaitForSeconds _waitForSeconds;
    private WaitUntil _waitUntilNextClick;
    private bool _playerClickLeftMouseButton = false;

    public event Action<Soldier> SoldierSelected;

    private void Awake()
    {
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
        if (_soldierSelector.TrySelectSoldier(out Soldier soldier))
        {
            SoldierSelected?.Invoke(soldier);
        }
        else
        {
            Debug.Log("Soldier isn't selected");
            yield break;
        }

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

        _playerClickLeftMouseButton = false;
        _coroutine = null;
    }

    private void ClickLeftMouseButton(InputAction.CallbackContext context)
    {
        _playerClickLeftMouseButton = true;
    }
}
