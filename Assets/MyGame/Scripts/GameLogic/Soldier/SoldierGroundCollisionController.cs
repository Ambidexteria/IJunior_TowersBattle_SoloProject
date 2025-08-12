using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SoldierGroundCollisionController : MonoBehaviour
{
    [SerializeField] private Collider _groundCollider;

    public void Enable()
    {
        _groundCollider.enabled = true;
    }

    public void Disable()
    {
        _groundCollider.enabled = false;
    }
}
