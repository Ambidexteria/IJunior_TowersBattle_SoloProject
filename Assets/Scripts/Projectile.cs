using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody> ();
    }
}
