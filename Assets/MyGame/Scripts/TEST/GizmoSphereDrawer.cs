using UnityEngine;

public class GizmoSphereDrawer : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private float _radius;

    private Color _defaultColor;

    private void OnDrawGizmos()
    {
        _defaultColor = Gizmos.color;
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
        Gizmos.color = _defaultColor;
    }
}
