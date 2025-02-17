using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Soldier : SpawnableObject, IDamageable, IMovable
{
    [SerializeField] private SoldierMoverToTarget _moverToTarget;
    [SerializeField] private Animator _animator;
    [SerializeField] private SoldierWeapon _weapon;

    private Rigidbody _rigidbody;

    public Animator Animator => _animator;
    public SoldierMoverToTarget MoverToTarget => _moverToTarget;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveTo(Transform taget)
    {
        _moverToTarget.MoveTo(taget);
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

    public void Attack(IDamageable enemySoldier)
    {
        _weapon.Attack(enemySoldier);
    }

    public void StopAttack()
    {
        _weapon.StopAttack();
    }

    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }

    public bool IsDead()
    {
        return false;
    }
}
