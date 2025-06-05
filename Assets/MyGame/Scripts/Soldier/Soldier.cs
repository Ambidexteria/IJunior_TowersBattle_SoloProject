using Base.Health;
using Base.Infrastructure;
using Base.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Soldier : SpawnableObject, ISoldier, IMovable, IAttacker
{
    [SerializeField] private SoldierGroundCollisionController _groundCollisionController;
    [SerializeField] private Animator _animator;
    [SerializeField] private SoldierWeapon _weapon;
    [SerializeField] private TriggerObserver _enemyTrigger;
    [SerializeField] private TriggerObserver _despawnerTrigger;
    [SerializeField] private TeamColorChanger _colorChanger;
    [SerializeField] private float _dieDelay;
    [SerializeField] private List<ColorChangerMark> _marks;

    private HealthModel _health;
    private RotatorToTarget _rotatorToTarget;
    private DespawnerDetector _despawnerDetector;
    private SoldierStateMachine _stateMachine;
    private SoldierMoverToTarget _moverToTarget;
    private TargetDetector _enemiesDetector;
    private Rigidbody _rigidbody;
    private Team _team;
    private WaitForSeconds _waitToDie;

    public bool IsIdle => _stateMachine.IsIdle;

    public event Action<Transform> MovingToTarget;
    public event Action<ISoldier> EnemyTargetDetected;
    public event Action Dying;
    public event Action<Soldier> DespawnerDetected;

    private void Awake()
    {
        _waitToDie = new WaitForSeconds(_dieDelay);
    }

    private void OnEnable()
    {
        _stateMachine.Enable();

        _health.Increase(_health.MaxValue);
        _groundCollisionController.Enable();

        _despawnerDetector.Detected += OnDespawnerDetected;
        _enemiesDetector.Detected += OnEnemyTargetDetected;
        _health.Dying += Die;
    }

    private void OnDisable()
    {
        _stateMachine.Disable();

        _despawnerDetector.Detected -= OnDespawnerDetected;
        _enemiesDetector.Detected -= OnEnemyTargetDetected;
        _health.Dying -= Die;
    }

    private void Update()
    {
        _stateMachine.Update();
        _moverToTarget.Update();
    }

    [Inject]
    private void Init(SoldierStats soldierStats, TeamColorChanger teamColorChanger, ICoroutineRunner coroutineRunner)
    {
        _colorChanger = teamColorChanger;
        _rigidbody = GetComponent<Rigidbody>();

        _despawnerDetector = new DespawnerDetector(_despawnerTrigger);
        _rotatorToTarget = new RotatorToTarget(transform);
        _enemiesDetector = new TargetDetector(_enemyTrigger);
        _stateMachine = new SoldierStateMachine(_animator, this);
        _moverToTarget = new SoldierMoverToTarget(_rigidbody, soldierStats);
        _health = new HealthModel(soldierStats.MaxHealth, coroutineRunner);
    }

    public void SetTeam(Team team)
    {
        _team = team;

        _colorChanger.Recolor(team, _marks);
        _enemiesDetector.SetTeam(team);
        _enemiesDetector.Enable();
        _weapon.SetTeam(team);
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
        return transform;
    }

    public TeamType GetTeam()
    {
        return _team.Type;
    }

    public bool TryGetNextAttackTarget(out ISoldier target)
    {
        return _enemiesDetector.TryGetNextAttackTarget(out target);
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

        StartCoroutine(DieCoroutine());
    }

    private void OnDespawnerDetected()
    {
        DespawnerDetected?.Invoke(this);
    }

    private IEnumerator DieCoroutine()
    {
        yield return _waitToDie;

        _groundCollisionController.Disable();
        _rigidbody.WakeUp();

        while (enabled)
        {
            _rigidbody.velocity += Physics.gravity * Time.deltaTime;
            yield return null;
        }
    }
}
