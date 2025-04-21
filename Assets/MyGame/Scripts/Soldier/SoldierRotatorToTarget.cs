using UnityEngine;

public class SoldierRotatorToTarget : MonoBehaviour
{
    public void RotateAroundYAxisTo(Transform target)
    {
        float checkRoationDegree = 1f;
        float angleBefore = GetAngleTo(target);

        transform.Rotate(0, checkRoationDegree, 0, Space.World);

        float angleAfter = GetAngleTo(target);

        if (angleAfter > angleBefore)
            angleAfter *= -1;

        transform.Rotate(0, angleAfter, 0, Space.World);
    }

    private float GetAngleTo(Transform target)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(target.position - transform.position, Vector3.up);
        return Vector3.Angle(transform.forward, projectedVector);
    }
}
