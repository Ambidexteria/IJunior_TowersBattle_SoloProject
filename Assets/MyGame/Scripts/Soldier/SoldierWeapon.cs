using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class SoldierWeapon : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private float _startDelay;
    [SerializeField] private Transform _barrel;

    private float _damage = 1f;
    private Team _team;
    private ProjectileSpawner _projectileSpawner;

    private Coroutine _coroutine;
    private WaitForSeconds _waitCooldown;
    private WaitForSeconds _waitStartDelay;
    private bool _isTargetAlive = false;

    public bool IsTargetAlive => _isTargetAlive;

    public event Action TargetDestroyed;

    private void Awake()
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierWeapon), nameof(Awake), _barrel);

        _waitCooldown = new WaitForSeconds(_shootCooldown);
        _waitStartDelay = new WaitForSeconds(_startDelay);
    }

    [Inject]
    private void Init(ProjectileSpawner spawner)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierWeapon), nameof(Init), spawner);

        _projectileSpawner = spawner;
    }

    public void Init(Team team, float damage)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierWeapon), nameof(Init), team);

        _team = team;
        _damage = damage;
    }

    public void Attack(ISoldier soldier)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierWeapon), nameof(Attack), soldier);

        if (soldier.GetTeam() == _team.Type)
            return;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _isTargetAlive = true;
        _coroutine = StartCoroutine(Shoot(soldier));
    }

    public void StopAttack()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
        _isTargetAlive = false;
    }

    private IEnumerator Shoot(ISoldier target)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierWeapon), nameof(Shoot), target);

        yield return _waitStartDelay;

        while (_isTargetAlive)
        {
            Projectile projectile = _projectileSpawner.Spawn();

            projectile.Init(_team.Type, _damage);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _barrel.position;
            projectile.Rigidbody.velocity = (target.GetTransform().position - projectile.transform.position) * _projectileSpeed;

            if (target.IsDead())
            {
                _isTargetAlive = false;
                TargetDestroyed?.Invoke();
            }

            yield return _waitCooldown;
        }
    }
}
