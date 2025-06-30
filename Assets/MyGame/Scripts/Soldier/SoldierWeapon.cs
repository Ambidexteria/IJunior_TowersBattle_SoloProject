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

    private Team _team;
    private ProjectileSpawner _projectileSpawner;

    private Coroutine _coroutine;
    private WaitForSeconds _waitCooldown;
    private WaitForSeconds _waitStartDelay;
    private bool _isTargetAlive = false;

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

    public void SetTeam(Team team)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierWeapon), nameof(SetTeam), team);

        _team = team;
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
    }

    private IEnumerator Shoot(ISoldier target)
    {
        ExceptionsTest.NullRefMethodTest(nameof(SoldierWeapon), nameof(Shoot), target);

        yield return _waitStartDelay;

        while (_isTargetAlive)
        {
            Projectile projectile = _projectileSpawner.Spawn();

            projectile.Init(_team.Type);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _barrel.transform.position;
            projectile.Rigidbody.velocity = (target.GetTransform().position - _barrel.position) * _projectileSpeed;

            if (target.IsDead())
            {
                _isTargetAlive = false;
                TargetDestroyed?.Invoke();
            }

            yield return _waitCooldown;
        }
    }
}
