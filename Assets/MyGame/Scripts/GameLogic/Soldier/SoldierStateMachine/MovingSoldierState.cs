using System;
using UnityEngine;

public class MovingSoldierState : ISoldierState
{
    private readonly Animator _animator;
    private readonly IMovable _movable;

    public MovingSoldierState(Animator animator, IMovable moverToTarget)
    {
        _animator = animator;
        _movable = moverToTarget;
    }

    public event Action TargetReached;

    public void OnStart(SoldierStateContext context)
    {
        _animator.SetTrigger(SoldierAnimationTriggerNames.IdleToMove);
    }

    public void OnStop()
    {
        _movable.Stop();

        _animator.SetTrigger(SoldierAnimationTriggerNames.MoveToIdle);
    }

    public void OnUpdate()
    {
        if(_movable.TargetReached())
            TargetReached?.Invoke();
    }
}
