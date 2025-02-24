using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Soldier))]
public class SoldierStateMachine : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private TestAttackTarget _attackTarget;

    private Soldier _soldier;
    private ISoldierState _currentState;
    private SoldierStateType _currentStateType;
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
        _soldier.MovingToTarget += SetMoveState;
        _soldier.AttackingTarget += SetAttackState;
    }

    private void OnDisable()
    {
        _soldier.MovingToTarget -= SetMoveState;
        _soldier.AttackingTarget -= SetAttackState;
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState.OnUpdate();

        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    _context.MoveTarget = _target;
        //    ChangeState(_soldierStates[SoldierStateType.Move]);
        //}

        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    ChangeState(_soldierStates[SoldierStateType.Idle]);
        //}
        
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    _context.AttackTarget = _attackTarget;
        //    ChangeState(_soldierStates[SoldierStateType.Attack]);
        //}
    }

    private void SetIdleState()
    {
        ChangeState(SoldierStateType.Idle);
    }

    private void SetAttackState(IDamageable damageable)
    {
        _context.AttackTarget = damageable;
        ChangeState(SoldierStateType.Attack);
    }

    private void SetMoveState(Transform target)
    {
        _context.MoveTarget = target;
        ChangeState(SoldierStateType.Move);
    }

    private void ChangeState(SoldierStateType stateType)
    {
        if (_currentStateType == stateType)
            return;
        else
            _currentStateType = stateType;

        ISoldierState nextState = _soldierStates[stateType];

        if (_currentState != null)
        {
            _currentState.OnStop();
        }

        _currentState = nextState;
        _currentState.OnStart(_context);
    }
}
