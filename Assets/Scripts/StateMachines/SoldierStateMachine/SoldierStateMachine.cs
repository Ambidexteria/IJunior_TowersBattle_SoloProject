using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Soldier))]
public class SoldierStateMachine : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private TestAttackTarget _attackTarget;

    private Soldier _soldier;
    private ISoldierState _currentState;
    private Dictionary<SoldierStateType, ISoldierState> _soldierStates;
    private SoldierStateContext _context;

    private void Awake()
    {
        _soldier = GetComponent<Soldier>();
        _context = new SoldierStateContext();

        MovingSoldierState moveState = new MovingSoldierState(_soldier.Animator, _soldier);
        AttackSoldierState attackState = new AttackSoldierState(_soldier.Animator, _soldier);
        moveState.TargetReached += SetIdleState;

        _soldierStates = new Dictionary<SoldierStateType, ISoldierState>
        {
            {SoldierStateType.Idle, new IdleSoldierState(_soldier.Animator) },
            {SoldierStateType.Move, moveState },
            {SoldierStateType.Attack, attackState }
        };

        SetIdleState();
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
        
        if (Input.GetKeyUp(KeyCode.A))
        {
            _context.AttackTarget = _attackTarget;
            ChangeState(_soldierStates[SoldierStateType.Attack]);
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
