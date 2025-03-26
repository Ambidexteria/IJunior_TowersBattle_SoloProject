using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Team))]
public class Cannon : MonoBehaviour, IDamageable
{
    private const string BarrelDrawback = nameof(BarrelDrawback);

    [SerializeField] private Animator _animator;
    [SerializeField] private TeamColorChanger _colorChanger;
    [SerializeField] private Cannon _enemyCannon;
    [SerializeField] private Health _health;
    [SerializeField] private ParticleSystemController _shootEffect;
    [SerializeField] private ParticleSystemController _takeDamageEffect;
    [SerializeField] private Barrel _barrel;
    [SerializeField] private int _damage;
    [SerializeField] private float _fireDelay;

    private Team _team;
    private CannonProjectileSpawner _projectileSpawner;

    public int Damage => _damage;

    public event Action Destroyed;

    private void Awake()
    {
        _team = GetComponent<Team>();
        _colorChanger.Recolor(_team);
    }

    private void OnEnable()
    {
        _health.Dying += OnDying;
    }

    private void OnDisable()
    {
        _health.Dying -= OnDying;
    }

    [Inject]
    private void Init(CannonProjectileSpawner projectileSpawner)
    {
        _projectileSpawner = projectileSpawner;
    }

    public TeamType GetTeamType() => _team.Type;

    public bool IsDead()
    {
        return _health.IsDead;
    }

    public void Shoot()
    {
        CannonProjectile cannonProjectile = _projectileSpawner.Spawn();

        cannonProjectile.transform.position = _barrel.StartPoint;
        cannonProjectile.Init(_team, _barrel.StartPoint, _enemyCannon.transform.position, _damage);
        cannonProjectile.gameObject.SetActive(true);

        _shootEffect.Play();
        _animator.Play(BarrelDrawback);
    }

    public void TakeDamage(int amount)
    {
        _health.Decrease(amount);
        _takeDamageEffect.Play();
    }

    private void OnDying()
    {
        Destroyed?.Invoke();
    }
}
