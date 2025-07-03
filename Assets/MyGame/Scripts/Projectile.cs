using Base.Soldier;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : SpawnableObject
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _lifeTime = 3f;

    private float _damage = 1f;
    private TeamType _team;

    private float _currentTime;

    public Rigidbody Rigidbody => _rigidbody;

    public event Action<Projectile> Despawning;

    private void Awake()
    {
        ExceptionsTest.NullRefMethodTest(nameof(Projectile), nameof(Awake), _rigidbody);
    }

    private void OnEnable()
    {
        _currentTime = 0f;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _lifeTime)
        {
            Despawning?.Invoke(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ExceptionsTest.NullRefMethodTest(nameof(Projectile), nameof(OnTriggerEnter), other);

        if (other.TryGetComponent(out SoldierSetup setup))
        {
            if (setup.GetSoldier().GetTeam() != _team)
            {
                setup.GetSoldier().TakeDamage(_damage);
                Despawning?.Invoke(this);
            }
        }
    }

    public void Init(TeamType team, float damage)
    {
        _team = team;
        _damage = damage;
    }
}
