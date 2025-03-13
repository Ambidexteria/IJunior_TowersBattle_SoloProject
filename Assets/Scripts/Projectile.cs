using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public class Projectile : SpawnableObject
{
    private const string ProjectilesParent = nameof(ProjectilesParent);

    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 3f;

    private Rigidbody _rigidbody;
    private TeamType _team;

    private float _currentTime;

    public Rigidbody Rigidbody => _rigidbody;

    public event Action<Projectile> Despawning;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        if (other.TryGetComponent(out ITargetSoldier damageable))
        {
            if(damageable.GetTeam() != _team)
            {
                damageable.TakeDamage(_damage);
                Despawning?.Invoke(this);
            }
        }
    }

    public void Init(TeamType team)
    {
        _team = team;
    }
}
