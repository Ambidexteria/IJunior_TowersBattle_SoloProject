using System.Collections.Generic;
using UnityEngine;

public class SoldierStateMachine
{
    private ISoldier _soldier;
    private Animator _animator;
    private ISoldierState _currentState;
    private SoldierStateType _currentStateType;
    private Dictionary<SoldierStateType, ISoldierState> _soldierStates;
    private SoldierStateContext _context;
    private SoldierStateType _previousStateType;
    private bool _isActive = true;

    public bool IsIdle => _currentStateType == SoldierStateType.Idle;

    public SoldierStateMachine(Animator animator, ISoldier solder)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(SoldierStateMachine), animator, solder);

        _animator = animator;
        _soldier = solder;

        InitializeStatesDictionary();
    }

    public void Enable()
    {
        _isActive = true;
        SetIdleState();

        _soldier.MovingToTarget += SetMoveState;
        _soldier.EnemyTargetDetected += SetAttackState;
        _soldier.Dying += SetDieState;
    }

    public void Disable()
    {
        _soldier.MovingToTarget -= SetMoveState;
        _soldier.EnemyTargetDetected -= SetAttackState;
        _soldier.Dying -= SetDieState;
    }

    public void Update()
    {
        if (_isActive)
            if (_currentState != null)
                _currentState.OnUpdate();
    }

    private void Deactivate()
    {
        _isActive = false;
    }

    private void SetIdleState()
    {
        ChangeState(SoldierStateType.Idle);
    }

    private void SetAttackState(ISoldier target)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierStateMachine), nameof(SetAttackState), target);

        if (_currentStateType == SoldierStateType.Attack)
            return;

        _context.AttackTarget = target;
        _previousStateType = _currentStateType;

        ChangeState(SoldierStateType.Attack);
    }

    private void SetMoveState(Transform target)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierStateMachine), nameof(SetMoveState), target);

        _context.MoveTarget = target;
        ChangeState(SoldierStateType.Move);
    }

    private void SetDieState()
    {
        ChangeState(SoldierStateType.Die); 
    }

    private void ChangeState(SoldierStateType stateType)
    {
        if (_isActive == false)
            return;

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

    private void ReturnToPreviousState()
    {
        if (_previousStateType == SoldierStateType.Move)
            _soldier.MoveTo(_context.MoveTarget);

        if (_previousStateType == SoldierStateType.Idle)
            SetIdleState();
    }

    private void InitializeStatesDictionary()
    {
        MovingSoldierState moveState = new (_animator, _soldier);
        AttackSoldierState attackState = new (_animator, _soldier, _soldier);
        DieSoldierState dieState = new (_animator);
        IdleSoldierState idleState = new (_animator);

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


    [ContextMenu(nameof(ShowCurrentState))]
    private void ShowCurrentState()
    {
        string currentStateName = "null";

        if (_currentStateType == SoldierStateType.Move)
            currentStateName = nameof(SoldierStateType.Move);
        else if (_currentStateType == SoldierStateType.Idle)
            currentStateName = nameof(SoldierStateType.Idle);
        else if (_currentStateType == SoldierStateType.Attack)
        {
            currentStateName = nameof(SoldierStateType.Attack);
            Debug.Log($"{_context.AttackTarget} - Attack target");
            Debug.Log($"{nameof(_isActive)} = {_isActive == true}");
        }
        else if (_currentStateType == SoldierStateType.Die)
            currentStateName = nameof(SoldierStateType.Die);

        Debug.Log($"{_animator.transform.root.name} --- current state: {currentStateName}");
    }
}
