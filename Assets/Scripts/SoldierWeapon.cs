using System.Collections;
using UnityEngine;

public class SoldierWeapon : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private float _startDelay;
    [SerializeField] private Transform _barrel;

    private Coroutine _coroutine;
    private WaitForSeconds _waitCooldown;
    private WaitForSeconds _waitStartDelay;

    private void Awake()
    {
        _waitCooldown = new WaitForSeconds(_shootCooldown);
        _waitStartDelay = new WaitForSeconds(_startDelay);
    }

    public void Attack(ITargetSoldier damageable)
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(Shoot(damageable));
    }

    public void StopAttack()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator Shoot(ITargetSoldier target)
    {
        yield return _waitStartDelay;

        while (target.IsDead() == false)
        {
            Projectile projectile = Instantiate(_projectilePrefab, _barrel.transform.position, Quaternion.identity);

            projectile.Rigidbody.velocity = _barrel.forward * _projectileSpeed;

            yield return _waitCooldown;
        }
    }
}
