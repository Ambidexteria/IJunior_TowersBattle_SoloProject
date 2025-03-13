using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class SoldierWeapon : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
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
        _waitCooldown = new WaitForSeconds(_shootCooldown);
        _waitStartDelay = new WaitForSeconds(_startDelay);
    }

    [Inject]
    private void Init(ProjectileSpawner spawner)
    {
        _projectileSpawner = spawner;
    }

    public void SetTeam(Team team)
    {
        _team = team;
    }

    public void Attack(ITargetSoldier damageable)
    {
        if (damageable.GetTeam() == _team.Type)
            return;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _isTargetAlive = true;
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

        while (_isTargetAlive)
        {
            Projectile projectile = _projectileSpawner.Spawn();

            projectile.Init(_team.Type);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _barrel.transform.position;
            projectile.Rigidbody.velocity = _barrel.forward * _projectileSpeed;

            if (target.IsDead())
            {
                _isTargetAlive = false;
                TargetDestroyed?.Invoke();
            }

            yield return _waitCooldown;
        }
    }
}
