using Base.Data.Game;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SoldierMoverToTarget
{
    private readonly float _speed;
    private readonly float _brakeSpeed;
    private readonly Rigidbody _rigidbody;
    private readonly float _minDistanceSqr;

    private Transform _target;
    private bool _isStopped = true;

    public SoldierMoverToTarget(Rigidbody rigidbody, SoldierData stats)
    {
        _rigidbody = rigidbody;
        _speed = stats.Speed;
        _brakeSpeed = stats.BrakeSpeed;
        _minDistanceSqr = stats.MinDistanceToTarget * stats.MinDistanceToTarget;
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
