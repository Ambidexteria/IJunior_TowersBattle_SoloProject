using UnityEngine;

public class CameraBordersGizmoDrawer : MonoBehaviour
{
    private const float SizeMultiplyer = 2.0f;

    [SerializeField] private Color color = Color.red;

    private Camera _camera;

    private void OnDrawGizmos()
    {
        if (_camera == null)
            _camera = gameObject.GetComponent<Camera>();

        Color tempColor = Gizmos.color;
        Matrix4x4 tempMat = Gizmos.matrix;
        Gizmos.color = color;

        if (_camera.orthographic)
        {
            Camera c = _camera;
            var size = c.orthographicSize;
            Gizmos.DrawWireCube(
                Vector3.forward * (c.nearClipPlane + (c.farClipPlane - c.nearClipPlane) / 2),
                new Vector3(size * SizeMultiplyer, size * SizeMultiplyer * c.aspect, c.farClipPlane - c.nearClipPlane));
        }
        else
        {
            Camera c = _camera;
            Vector3 center = Vector3.zero;

            if (c.usePhysicalProperties)
                center = c.lensShift;

            Gizmos.matrix = Matrix4x4.TRS(this.transform.position, this.transform.rotation, Vector3.one);
            Gizmos.DrawFrustum(center, c.fieldOfView, c.farClipPlane, c.nearClipPlane, c.aspect);
        }

        Gizmos.color = tempColor;
        Gizmos.matrix = tempMat;
    }
}
