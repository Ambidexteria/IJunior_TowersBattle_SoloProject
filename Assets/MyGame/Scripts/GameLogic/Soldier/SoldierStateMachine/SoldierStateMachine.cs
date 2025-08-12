using System.Collections.Generic;
using UnityEngine;

public class SoldierStateMachine
{
    private readonly ISoldier _soldier;
    private readonly Animator _animator;

    private ISoldierState _currentState;
    private SoldierStateType _currentStateType;
    private Dictionary<SoldierStateType, ISoldierState> _soldierStates;
    private SoldierStateContext _context;
    private SoldierStateType _previousStateType;
    private bool _enabled = false;

    public bool IsIdle => _currentStateType == SoldierStateType.Idle;

    public SoldierStateMachine(Animator animator, ISoldier solder)
    {
        _animator = animator;
        _soldier = solder;

        InitializeStatesDictionary();
    }

    public void Enable()
    {
        if (_enabled)
            return;

        _enabled = true;

        SetIdleState();

        _soldier.MovingToTarget += SetMoveState;
        _soldier.EnemyTargetDetected += SetAttackState;
        _soldier.Dying += SetDieState;
    }

    public void Disable()
    {
        if (_enabled == false)
            return;

        _enabled = false;

        _soldier.MovingToTarget -= SetMoveState;
        _soldier.EnemyTargetDetected -= SetAttackState;
        _soldier.Dying -= SetDieState;
    }

    public void Update()
    {
        if (_enabled)
            if (_currentState != null)
                _currentState.OnUpdate();
    }

    private void Deactivate()
    {
        _enabled = false;
    }

    private void SetIdleState()
    {
        ChangeState(SoldierStateType.Idle);
    }

    private void SetAttackState(ISoldier target)
    {
        if (_currentStateType == SoldierStateType.Attack)
            return;

        _context.AttackTarget = target;
        _previousStateType = _currentStateType;

        ChangeState(SoldierStateType.Attack);
    }

    private void SetMoveState(Transform target)
    {
        _context.MoveTarget = target;
        ChangeState(SoldierStateType.Move);
    }

    private void SetDieState()
    {
        ChangeState(SoldierStateType.Die);
    }

    private void ChangeState(SoldierStateType stateType)
    {
        if (_enabled == false)
            return;

        if (_currentStateType == stateType)
            return;
        else
            _currentStateType = stateType;

        ISoldierState nextState = _soldierStates[stateType];

        _currentState?.OnStop();

        _currentState = nextState;
        _currentState.OnStart(_context);
    }

    private void ReturnToPreviousState()
    {
        if (_previousStateType == SoldierStateType.Move)
            _soldier.MoveTo(_context.MoveTarget);

        if (_previousStateType == SoldierStateType.Idle)
            SetIdleState();
    }

    private void InitializeStatesDictionary()
    {
        MovingSoldierState moveState = new(_animator, _soldier);
        AttackSoldierState attackState = new(_animator, _soldier, _soldier);
        DieSoldierState dieState = new(_animator);
        IdleSoldierState idleState = new(_animator);

        moveState.TargetReached += SetIdleState;
        attackState.AllTargetsDestroyed += ReturnToPreviousState;
        dieState.Dying += Deactivate;

        _soldierStates = new Dictionary<SoldierStateType, ISoldierState>
        {
            {SoldierStateType.Idle, idleState },
            {SoldierStateType.Move, moveState },
            {SoldierStateType.Attack, attackState },
            {SoldierStateType.Die, dieState },
        };
    }
}
