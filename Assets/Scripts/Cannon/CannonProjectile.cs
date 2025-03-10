using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CannonProjectile : SpawnableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;
    [SerializeField] private SplineFollower _follower;
    [SerializeField] private TeamColorChanger _colorChanger;

    private Collider _collider;
    private Team _team;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cannon cannon))
        {
            if (cannon.GetTeamType() != _team.Type)
            {
                cannon.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

    public void Init(Team team, Vector3 start, Vector3 fifnish, int damage)
    {
        _team = team;
        _colorChanger.Recolor(_team);
        _damage = damage;
        _follower.StartMovement(_speed, start, fifnish);
        _collider.enabled = true;
    }
}
