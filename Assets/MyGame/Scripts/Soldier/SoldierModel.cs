using Base.Data.Game;
using Base.Health;
using Base.Infrastructure;
using Base.Logic;
using Base.Soldier;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierModel : ISoldier, IMovable, IAttacker
{
    private SoldierGroundCollisionController _groundCollisionController;
    private Animator _animator;
    private SoldierWeapon _weapon;
    private TriggerObserver _enemyTrigger;
    private TriggerObserver _despawnerTrigger;
    private TeamColorChanger _colorChanger;
    private readonly Transform _soldierTransform;
    private float _dieDelay;
    private List<ColorChangerMark> _marks;
    private HealthModel _health;
    private ICoroutineRunner _coroutineRunner;
    private RotatorToTarget _rotatorToTarget;
    private DespawnerDetector _despawnerDetector;
    private SoldierStateMachine _stateMachine;
    private SoldierMoverToTarget _moverToTarget;
    private TargetDetector _enemiesDetector;
    private Rigidbody _rigidbody;
    private Team _team;
    private WaitForSeconds _waitToDie;
    private Coroutine _coroutine;

    private bool _enabled = false;
    private Coroutine _dieCoroutine;

    public SoldierModel(SoldierGroundCollisionController groundCollisionController, Animator animator,
        SoldierWeapon weapon, TriggerObserver enemyTrigger, TriggerObserver despawnerTrigger,
        float dieDelay, List<ColorChangerMark> marks, Rigidbody rigidbody,
        Team team, SoldierData stats, ICoroutineRunner coroutineRunner, TeamColorChanger teamColorChanger, Transform soldierTransform)
    {
        _groundCollisionController = groundCollisionController;
        _animator = animator;
        _weapon = weapon;
        _enemyTrigger = enemyTrigger;
        _despawnerTrigger = despawnerTrigger;
        _dieDelay = dieDelay;
        _marks = marks;
        _rigidbody = rigidbody;
        _team = team;
        _coroutineRunner = coroutineRunner;
        _colorChanger = teamColorChanger;
        _soldierTransform = soldierTransform;
        _waitToDie = new WaitForSeconds(_dieDelay);
        _despawnerDetector = new DespawnerDetector(_despawnerTrigger);
        _rotatorToTarget = new RotatorToTarget(_soldierTransform);
        _enemiesDetector = new TargetDetector(_enemyTrigger, _team);
        _stateMachine = new SoldierStateMachine(_animator, this);
        _moverToTarget = new SoldierMoverToTarget(_rigidbody, stats);
        _health = new HealthModel(stats.MaxHealth, coroutineRunner);

        _colorChanger.Recolor(team, _marks);
        _weapon.SetTeam(_team);
    }

    public bool IsIdle => _stateMachine.IsIdle;

    public event Action<Transform> MovingToTarget;
    public event Action<ISoldier> EnemyTargetDetected;
    public event Action Dying;
    public event Action<SoldierSetup> DespawnerDetected;

    public void Enable()
    {
        if (_enabled)
            return;

        _enemiesDetector.Enable();
        _stateMachine.Enable();
        _groundCollisionController.Enable();

        _health.Increase(_health.MaxValue);

        _despawnerDetector.Detected += OnDespawnerDetected;
        _enemiesDetector.Detected += OnEnemyTargetDetected;
        _health.Dying += Die;

        _enabled = true;

        _coroutine = _coroutineRunner.LaunchCoroutine(UpdateCoroutine());
    }

    public void Disable()
    {
        if (_enabled == false)
            return;

        _stateMachine.Disable();
        _enemiesDetector.Disable();

        _despawnerDetector.Detected -= OnDespawnerDetected;
        _enemiesDetector.Detected -= OnEnemyTargetDetected;
        _health.Dying -= Die;

        if (_coroutine != null)
            _coroutineRunner.EndCoroutine(_coroutine);

        if (_dieCoroutine != null)
            _coroutineRunner.EndCoroutine(_dieCoroutine);

        _enabled = false;
    }

    public void MoveTo(Transform target)
    {
        MovingToTarget?.Invoke(target);
        _moverToTarget.MoveTo(target);
        _rotatorToTarget.RotateAroundYAxisTo(target);
    }

    public void Stop()
    {
        _moverToTarget.Stop();
    }

    public bool TargetReached()
    {
        return _moverToTarget.TargetReached();
    }

    public void Attack(ISoldier enemySoldier)
    {
        _weapon.Attack(enemySoldier);
        _rotatorToTarget.RotateAroundYAxisTo(enemySoldier.GetTransform());
    }

    public void StopAttack()
    {
        _weapon.StopAttack();
    }

    public void TakeDamage(int amount)
    {
        _health.Decrease(amount);
    }

    public bool IsDead()
    {
        return _health.IsDead;
    }

    public Transform GetTransform()
    {
        return _soldierTransform;
    }

    public TeamType GetTeam()
    {
        return _team.Type;
    }

    public bool TryGetNextAttackTarget(out ISoldier target)
    {
        return _enemiesDetector.TryGetNextAttackTarget(out target);
    }

    private IEnumerator UpdateCoroutine()
    {
        while (_enabled)
        {
            _stateMachine.Update();
            _moverToTarget.Update();
            yield return null;
        }
    }

    private void OnEnemyTargetDetected(ISoldier soldier)
    {
        if (soldier == null)
            return;

        EnemyTargetDetected?.Invoke(soldier);
    }

    private void Die()
    {
        Dying?.Invoke();
        StopAttack();
        Stop();

        _dieCoroutine = _coroutineRunner.LaunchCoroutine(DieCoroutine());
    }

    private void OnDespawnerDetected()
    {
        DespawnerDetected?.Invoke(_soldierTransform.GetComponent<SoldierSetup>());
    }

    private IEnumerator DieCoroutine()
    {
        yield return _waitToDie;

        _groundCollisionController.Disable();
        _rigidbody.WakeUp();

        while (_enabled)
        {
            _rigidbody.velocity += Physics.gravity * Time.deltaTime;
            yield return null;
        }
    }
}
