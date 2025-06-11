using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SoldierGroundCollisionController : MonoBehaviour
{
    private Collider _groundCollider;

    private void Awake()
    {
        _groundCollider = GetComponent<Collider>();
    }

    public void Enable()
    {
        _groundCollider.enabled = true;
    }

    public void Disable()
    {
        _groundCollider.enabled = false;
    }
}
