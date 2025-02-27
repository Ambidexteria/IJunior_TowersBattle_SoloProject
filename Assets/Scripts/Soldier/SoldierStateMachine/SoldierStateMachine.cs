using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Soldier))]
public class SoldierStateMachine : MonoBehaviour
{
    private Soldier _soldier;
    private ISoldierState _currentState;
    private SoldierStateType _currentStateType;
    private Dictionary<SoldierStateType, ISoldierState> _soldierStates;
    private SoldierStateContext _context;

    private SoldierStateType _previousStateType;
    private bool _isActive = true;

    private void Awake()
    {
        _soldier = GetComponent<Soldier>();
        _context = new SoldierStateContext();

        MovingSoldierState moveState = new MovingSoldierState(_soldier.Animator, _soldier);
        AttackSoldierState attackState = new AttackSoldierState(_soldier.Animator, _soldier, _soldier);
        DieSoldierState dieState = new DieSoldierState(_soldier.Animator);

        moveState.TargetReached += SetIdleState;
        attackState.AllTargetsDestroyed += ReturnToPreviousState;
        dieState.Dying += Deactivate;

        _soldierStates = new Dictionary<SoldierStateType, ISoldierState>
        {
            {SoldierStateType.Idle, new IdleSoldierState(_soldier.Animator) },
            {SoldierStateType.Move, moveState },
            {SoldierStateType.Attack, attackState },
            {SoldierStateType.Die, dieState },
        };

        SetIdleState();
    }

    private void OnEnable()
    {
        _soldier.MovingToTarget += SetMoveState;
        _soldier.EnemyTargetDetected += SetAttackState;
        _soldier.Dying += SetDieState;
    }

    private void OnDisable()
    {
        _soldier.MovingToTarget -= SetMoveState;
        _soldier.EnemyTargetDetected -= SetAttackState;
        _soldier.Dying -= SetDieState;
    }

    private void Update()
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

    private void SetAttackState(ITargetSoldier target)
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
            ChangeState(SoldierStateType.Idle);

        Debug.Log(nameof(ReturnToPreviousState));
    }
}
