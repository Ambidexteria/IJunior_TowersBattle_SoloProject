using UnityEngine;

[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_damage);

        Destroy(gameObject);
    }
}
