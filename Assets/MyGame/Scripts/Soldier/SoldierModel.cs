using Base.Data.Game;
using Base.Health;
using Base.Infrastructure;
using Base.Logic;
using Base.Soldier;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierModel : ISoldier
{
    private readonly SoldierGroundCollisionController _groundCollisionController;
    private readonly Animator _animator;
    private readonly SoldierWeapon _weapon;
    private readonly TriggerObserver _enemyTrigger;
    private readonly TriggerObserver _despawnerTrigger;
    private readonly TeamColorChanger _colorChanger;
    private readonly Transform _soldierTransform;
    private readonly float _dieDelay;
    private readonly List<ColorChangerMark> _marks;
    private readonly HealthModel _health;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly RotatorToTarget _rotatorToTarget;
    private readonly DespawnerDetector _despawnerDetector;
    private readonly SoldierStateMachine _stateMachine;
    private readonly SoldierMoverToTarget _moverToTarget;
    private readonly TargetDetector _enemiesDetector;
    private readonly Rigidbody _rigidbody;
    private readonly Team _team;
    private readonly WaitForSeconds _waitToDie;

    private Coroutine _updateCoroutine;
    private bool _enabled = false;
    private Coroutine _dieCoroutine;

    public SoldierModel(SoldierGroundCollisionController groundCollisionController, Animator animator,
        SoldierWeapon weapon, TriggerObserver enemyTrigger, TriggerObserver despawnerTrigger,
        float dieDelay, List<ColorChangerMark> marks, Rigidbody rigidbody,
        Team team, SoldierData stats, ICoroutineRunner coroutineRunner,
        TeamColorChanger teamColorChanger, Transform soldierTransform)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(SoldierModel), groundCollisionController, animator,
            weapon, enemyTrigger, despawnerTrigger, marks, rigidbody, team, stats, coroutineRunner, 
            teamColorChanger, soldierTransform);
        ExceptionsTest.EmptyListTest(nameof(SoldierModel), ExceptionsTest.ConstructorName, marks);

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
        _weapon.Init(_team, stats.Damage);
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

        _updateCoroutine = _coroutineRunner.LaunchCoroutine(UpdateCoroutine());
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

        if (_updateCoroutine != null)
            _coroutineRunner.EndCoroutine(_updateCoroutine);

        if (_dieCoroutine != null)
            _coroutineRunner.EndCoroutine(_dieCoroutine);

        _enabled = false;
    }

    public void MoveTo(Transform target)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierModel), nameof(MoveTo), target);

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
        ExceptionsTest.NullRefMethodTest(nameof(SoldierModel), nameof(Attack), enemySoldier);

        _weapon.Attack(enemySoldier);
        _rotatorToTarget.RotateAroundYAxisTo(enemySoldier.GetTransform());
    }

    public void StopAttack()
    {
        _weapon.StopAttack();
    }

    public void TakeDamage(float amount)
    {
        _health.Decrease(amount);
    }

    public bool IsDead()
    {
        return _health.IsDead;
    }

    public bool IsAttacking()
    {
        return _weapon.IsTargetAlive;
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
        ExceptionsTest.NullRefMethodTest(nameof(SoldierModel), nameof(Attack), soldier);

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
