using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Team))]
public class Cannon : MonoBehaviour, IDamageable
{
    [SerializeField] private TeamColorChanger _colorChanger;
    [SerializeField] private Cannon _enemyCannon;
    [SerializeField] private Health _health;
    [SerializeField] private CannonEnergyBar _energyBar;
    [SerializeField] private Barrel _barrel;
    [SerializeField] private int _damage;
    [SerializeField] private float _fireDelay;

    private Team _team;
    private CannonProjectileSpawner _projectileSpawner;

    public int Damage => _damage;

    public event Action EnergyBarFilled;

    private void Awake()
    {
        _team = GetComponent<Team>();
        _colorChanger.Recolor(_team);
    }

    private void OnEnable()
    {
        _energyBar.Filled += EnergyBarFilled;
    }

    private void OnDisable()
    {
        _energyBar.Filled -= EnergyBarFilled;
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

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        CannonProjectile cannonProjectile = _projectileSpawner.Spawn();

        cannonProjectile.transform.position = _barrel.StartPoint;
        cannonProjectile.Init(_team, _barrel.StartPoint, _enemyCannon.transform.position, _damage);
        cannonProjectile.gameObject.SetActive(true);
    }

    public void TakeDamage(int amount)
    {
        _health.Decrease(amount);
    }
}
