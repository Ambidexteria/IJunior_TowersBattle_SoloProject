using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Soldier))]
public class SoldierStateMachine : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Soldier _soldier;
    private ISoldierState _currentState;
    private Dictionary<SoldierStateType, ISoldierState> _soldierStates;
    private SoldierStateContext _context;

    private void Awake()
    {
        _soldier = GetComponent<Soldier>();
        _context = new SoldierStateContext();

        MovingSoldierState moveState = new MovingSoldierState(_soldier.Animator, _soldier.MoverToTarget);
        moveState.TargetReached += SetIdleState;

        _soldierStates = new Dictionary<SoldierStateType, ISoldierState>
        {
            {SoldierStateType.Idle, new IdleSoldierState(_soldier.Animator) },
            {SoldierStateType.Move, moveState }
        };


        //SetIdleState();    DONT FORGET TO TURN ON !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState.OnUpdate();

        if (Input.GetKeyUp(KeyCode.W))
        {
            _context.MoveTarget = _target;
            ChangeState(_soldierStates[SoldierStateType.Move]);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            ChangeState(_soldierStates[SoldierStateType.Idle]);
        }


    }

    private void SetIdleState()
    {
        ChangeState(_soldierStates[SoldierStateType.Idle]);
    }

    private void ChangeState(ISoldierState state)
    {
        if (_currentState != null)
        {
            _currentState.OnStop();
        }

        _currentState = state;
        _currentState.OnStart(_context);
    }
}
