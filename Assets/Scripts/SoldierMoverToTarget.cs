using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SoldierMoverToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistanceToTarget = 2f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _brakeSpeed = 1f;
    [SerializeField] private SlopeDetector _slopeDetector;
    [SerializeField] private float _slopeSlowingSpeedModifier = 1.5f;
    [SerializeField] private SoldierRotatorToTarget _rotatorToTarget;

    private Rigidbody _rigidbody;
    private float _minDistanceSqr;
    private Coroutine _brakingSpeedCoroutine;
    private bool _isStopped = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _minDistanceSqr = _minDistanceToTarget * _minDistanceToTarget;
    }

    private void Update()
    {
        if (_isStopped)
            return;

        if (_target == null)
            return;

        Move();

        _rotatorToTarget.RotateAroundYAxisTo(_target);
    }

    public void MoveTo(Transform target)
    {
        _target = target;
        _isStopped = false;
    }

    public void Stop()
    {
        _isStopped = true;

        if (_brakingSpeedCoroutine == null)
            _brakingSpeedCoroutine = StartCoroutine(BrakeSpeed());
    }

    public bool TargetReached()
    {
        return (_target.position - transform.position).sqrMagnitude < _minDistanceSqr;
    }

    private void Move()
    {
        Vector3 playerMoveDirection = (_target.position - transform.position).normalized;

        playerMoveDirection.y = 0;
        playerMoveDirection *= _speed;

        if (_slopeDetector.Detected)
        {
            playerMoveDirection += Vector3.down * _speed * _slopeSlowingSpeedModifier;
        }
        else
        {
            playerMoveDirection.y = _rigidbody.velocity.y;
        }

        _rigidbody.velocity = playerMoveDirection;
    }

    private IEnumerator BrakeSpeed()
    {
        while (_rigidbody.velocity.magnitude > 0.1f)
        {
            _rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, Vector3.zero, _brakeSpeed * Time.deltaTime);
            yield return null;
        }

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.Sleep();
    }
}
