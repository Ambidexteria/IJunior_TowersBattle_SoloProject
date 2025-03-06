using UnityEngine;

[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public class Projectile : SpawnableObject
{
    [SerializeField] private int _damage = 1;

    private Rigidbody _rigidbody;
    private TeamType _team;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITargetSoldier damageable))
        {
            if(damageable.GetTeam() != _team)
            {
                damageable.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

    public void Init(TeamType team)
    {
        _team = team;
    }
}
