using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Soldier : SpawnableObject, ITargetSoldier, IMovable, IAttacker
{
    [SerializeField] private SoldierMoverToTarget _moverToTarget; 
    [SerializeField] private SoldierRotatorToTarget _rotatorToTarget;
    [SerializeField] private Animator _animator;
    [SerializeField] private SoldierWeapon _weapon;
    [SerializeField] private Health _health;
    [SerializeField] private TargetDetector _detector;
    [SerializeField] private Team _team = Team.Player;

    private Rigidbody _rigidbody;

    public Animator Animator => _animator;
    public Team Team => _team;

    public event Action<Transform> MovingToTarget;
    public event Action<ITargetSoldier> AttackingTarget;
    public event Action Dying;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _detector.Detected += OnSoldierDetected;
        _health.Dying += Die;
    }

    private void OnDisable()
    {
        _detector.Detected -= OnSoldierDetected;
        _health.Dying -= Die;
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
        AttackingTarget?.Invoke(enemySoldier);
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

    public Team GetTeam()
    {
        return _team;
    }

    private void OnSoldierDetected(ITargetSoldier soldier)
    {
        if (soldier.GetTeam() == Team)
            return;

        Attack(soldier);
    }

    private void Die()
    {
        Dying?.Invoke();
        StopAttack();
        Stop();
        enabled = false;
    }
}
