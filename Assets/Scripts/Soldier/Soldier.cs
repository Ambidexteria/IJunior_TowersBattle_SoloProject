using System;
using UnityEngine;

[RequireComponent(typeof(Team))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(SoldierStateMachine))]
public class Soldier : SpawnableObject, ITargetSoldier, IMovable, IAttacker
{
    [SerializeField] private SoldierMoverToTarget _moverToTarget;
    [SerializeField] private SoldierRotatorToTarget _rotatorToTarget;
    [SerializeField] private Animator _animator;
    [SerializeField] private SoldierWeapon _weapon;
    [SerializeField] private Health _health;
    [SerializeField] private TargetDetector _enemiesDetector;
    [SerializeField] private TeamColorChanger _colorChanger;

    private Rigidbody _rigidbody;
    private Team _team;
    private SoldierStateMachine _stateMachine;

    public Animator Animator => _animator;
    public bool IsIdle => _stateMachine.IsIdle;

    public event Action<Transform> MovingToTarget;
    public event Action<ITargetSoldier> EnemyTargetDetected;
    public event Action Dying;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _team = GetComponent<Team>();
        _stateMachine = GetComponent<SoldierStateMachine>();
    }

    private void OnEnable()
    {
        _enemiesDetector.Detected += OnEnemyTargetDetected;
        _health.Dying += Die;
    }

    private void OnDisable()
    {
        _enemiesDetector.Detected -= OnEnemyTargetDetected;
        _health.Dying -= Die;
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
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.Sleep();
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
        EnemyTargetDetected?.Invoke(soldier);
    }

    private void Die()
    {
        Dying?.Invoke();
        StopAttack();
        Stop();
        enabled = false;
    }
}
