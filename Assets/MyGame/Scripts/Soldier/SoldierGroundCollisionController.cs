using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SoldierGroundCollisionController : MonoBehaviour
{
    private Collider _groundCollider;

    private void Awake()
    {
        _groundCollider = GetComponent<Collider>();

        ExceptionsTest.NullRefMethodTest(nameof(SoldierGroundCollisionController), nameof(Awake), _groundCollider);
    }

    public void Enable()
    {
        _groundCollider.enabled = true;
    }

    public void Disable()
    {
        Debug.Log($"{gameObject.transform.root.name} --- SoldierGroundCollider disabled");
        _groundCollider.enabled = false;
    }
}
