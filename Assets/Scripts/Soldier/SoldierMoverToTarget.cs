using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class SoldierMoverToTarget
{
    private float _minDistanceToTarget = 2f;
    private float _speed = 1f;
    private float _brakeSpeed = 1f;

    private Transform _target;
    private Rigidbody _rigidbody;
    private float _minDistanceSqr;
    private Coroutine _brakingSpeedCoroutine;
    private bool _isStopped = true;

    [Inject]
    public SoldierMoverToTarget(Rigidbody rigidbody, SoldierStats stats)
    {
        _rigidbody = rigidbody;

        _minDistanceToTarget = stats.MinDistanceToTarget;
        _speed = stats.Speed;
        _brakeSpeed = stats.BrakeSpeed;

        _minDistanceSqr = _minDistanceToTarget * _minDistanceToTarget;
    }

    public void Update()
    {
        if (_isStopped)
        {
            BrakeSpeed();
            return;
        }

        if (_target == null)
            return;

        Move();
    }

    public void MoveTo(Transform target)
    {
        _target = target;
        _isStopped = false;
    }

    public void Stop()
    {
        _isStopped = true;
    }

    public bool TargetReached()
    {
        return (_target.position - _rigidbody.transform.position).sqrMagnitude < _minDistanceSqr;
    }

    private void Move()
    {
        Vector3 playerMoveDirection = (_target.position - _rigidbody.transform.position).normalized;

        playerMoveDirection.y += Physics.gravity.y * Time.deltaTime;
        playerMoveDirection *= _speed;

        _rigidbody.velocity = playerMoveDirection;
    }

    private void BrakeSpeed()
    {
        if (_rigidbody.velocity.magnitude > 0.1f)
        {
            _rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, Vector3.zero, _brakeSpeed * Time.deltaTime);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.Sleep();
        }
    }
}
