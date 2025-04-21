using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _directionPoint;

    private Vector3 _direction;

    public Vector3 ShootDirection => _direction;
    public Vector3 StartPoint => _startPoint.position;

    private void Awake()
    {
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        _direction = (_directionPoint.position - _startPoint.position).normalized;
    }
}
