using System;
using System.Collections;
using Base.Services.Audio;
using UnityEngine;
using Zenject;

public class SoldierWeapon : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private float _startDelay;
    [SerializeField] private Transform _barrel;
    [SerializeField] private ParticleSystemController _firingEffect;

    private float _damage;
    private Team _team;
    private ProjectileSpawner _projectileSpawner;
    private AudioPlayerService _audioPlayer;
    private Coroutine _coroutine;
    private WaitForSeconds _waitCooldown;
    private WaitForSeconds _waitStartDelay;
    private bool _isTargetAlive = false;

    public event Action TargetDestroyed;

    public bool IsTargetAlive => _isTargetAlive;

    private void Awake()
    {
        _waitCooldown = new WaitForSeconds(_shootCooldown);
        _waitStartDelay = new WaitForSeconds(_startDelay);
    }

    [Inject]
    private void Init(ProjectileSpawner spawner, AudioPlayerService audioPlayer)
    {
        _projectileSpawner = spawner;
        _audioPlayer = audioPlayer;
    }

    public void Init(Team team, float damage)
    {
        _team = team;
        _damage = damage;
    }

    public void Attack(ISoldier soldier)
    {
        if (soldier.GetTeam() == _team.Type)
            return;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _isTargetAlive = true;
        _coroutine = StartCoroutine(Shoot(soldier));
        _firingEffect.Play();
    }

    public void StopAttack()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
        _isTargetAlive = false;
        _firingEffect.Stop();
    }

    private IEnumerator Shoot(ISoldier target)
    {
        yield return _waitStartDelay;

        while (_isTargetAlive)
        {
            Projectile projectile = _projectileSpawner.Spawn();

            projectile.Init(_team.Type, _damage);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _barrel.position;
            projectile.Rigidbody.velocity = (target.GetTransform().position - projectile.transform.position) * _projectileSpeed;

            _audioPlayer.PlaySoldierShootSound();

            if (target.IsDead())
            {
                _isTargetAlive = false;
                TargetDestroyed?.Invoke();
            }

            yield return _waitCooldown;
        }
    }
}
