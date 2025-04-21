using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Team))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Soldier : SpawnableObject, ITargetSoldier, IMovable, IAttacker
{
    [SerializeField] private SoldierGroundCollisionController _groundCollisionController;
    [SerializeField] private SoldierRotatorToTarget _rotatorToTarget;
    [SerializeField] private Animator _animator;
    [SerializeField] private SoldierWeapon _weapon;
    [SerializeField] private SoldierCollisionHandler _collisionHandler;
    [SerializeField] private TargetDetector _enemiesDetector;
    [SerializeField] private TeamColorChanger _colorChanger;
    [SerializeField] private float _dieDelay;

    private Health _health;
    private SoldierStateMachine _stateMachine;
    private SoldierMoverToTarget _moverToTarget;
    private Rigidbody _rigidbody;
    private Team _team;
    private WaitForSeconds _waitToDie;

    public Animator Animator => _animator;
    public bool IsIdle => _stateMachine.IsIdle;

    public event Action<Transform> MovingToTarget;
    public event Action<ITargetSoldier> EnemyTargetDetected;
    public event Action Dying;
    public event Action<Soldier> DespawnerDetected;

    private void Awake()
    {
        _team = GetComponent<Team>();

        _waitToDie = new WaitForSeconds(_dieDelay);
    }

    private void OnEnable()
    {
        _stateMachine.Enable();

        Debug.Log($"Soldier ienabled wibt {_health.MaxValue} of health");
        _health.Increase(_health.MaxValue);
        _groundCollisionController.Enable();

        _collisionHandler.DespawnerDetected += OnDespawnerDetected;
        _enemiesDetector.Detected += OnEnemyTargetDetected;
        _health.Dying += Die;
    }

    private void OnDisable()
    {
        _stateMachine.Disable();

        _collisionHandler.DespawnerDetected -= OnDespawnerDetected;
        _enemiesDetector.Detected -= OnEnemyTargetDetected;
        _health.Dying -= Die;
    }

    private void Update()
    {
        _stateMachine.Update();
        _moverToTarget.Update();
    }

    [Inject]
    private void Init(SoldierStats soldierStats)
    {
        _rigidbody = GetComponent<Rigidbody>();

        _stateMachine = new SoldierStateMachine(this);
        _moverToTarget = new SoldierMoverToTarget(_rigidbody, soldierStats);
        _health = new Health(soldierStats.MaxHealth);
    }

    public void SetTeam(Team team)
    {
        _team = team;

        _colorChanger.Recolor(team);
        _enemiesDetector.SetTeam(team);
        _enemiesDetector.gameObject.SetActive(true);
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

    public void Attack(ITargetSoldier enemySoldier)
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

    public bool TryGetNextAttackTarget(out ITargetSoldier target)
    {
        return _enemiesDetector.TryGetNextAttackTarget(out target);
    }

    private void OnEnemyTargetDetected(ITargetSoldier soldier)
    {
        if(soldier == null)
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
    }
}
