using UnityEngine;

public class RotatorToTarget
{
    private Transform _transform;

    public RotatorToTarget(Transform unitTransform)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(RotatorToTarget), unitTransform);

        _transform = unitTransform;
    }

    public void RotateAroundYAxisTo(Transform target)
    {
        ExceptionsTest.NullRefMethodTest(nameof(RotatorToTarget), nameof(RotateAroundYAxisTo), target);

        float checkRoationDegree = 1f;
        float angleBefore = GetAngleTo(target);

        _transform.Rotate(0, checkRoationDegree, 0, Space.World);

        float angleAfter = GetAngleTo(target);

        if (angleAfter > angleBefore)
            angleAfter *= -1;

        _transform.Rotate(0, angleAfter, 0, Space.World);
    }

    private float GetAngleTo(Transform target)
    {
        ExceptionsTest.NullRefMethodTest(nameof(RotatorToTarget), nameof(GetAngleTo), target);

        Vector3 projectedVector = Vector3.ProjectOnPlane(target.position - _transform.position, Vector3.up);
        return Vector3.Angle(_transform.forward, projectedVector);
    }
}
