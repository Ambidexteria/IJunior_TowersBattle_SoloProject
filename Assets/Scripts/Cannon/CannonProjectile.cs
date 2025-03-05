using SplineMesh;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CannonProjectile : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;
    [SerializeField] private SplineFollower _follower;

    [SerializeField] private Team _team = Team.None;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cannon cannon))
        {
            if (cannon.Team != _team)
            {
                Debug.Log("Triggered");
                cannon.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

    public void Init(Team team, Vector3 start, Vector3 fifnish, int damage)
    {
        Debug.Log("Init");
        _team = team;
        _damage = damage;
        _follower.StartMovement(_speed, start, fifnish);
    }
}
