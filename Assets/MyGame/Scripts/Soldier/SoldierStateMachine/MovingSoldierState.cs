using System;
using UnityEngine;

public class MovingSoldierState : ISoldierState
{
    private Animator _animator;
    private IMovable _movable;

    public MovingSoldierState(Animator animator, IMovable moverToTarget)
    {
        _animator = animator ?? throw new NullReferenceException(nameof(animator));
        _movable = moverToTarget ?? throw new NullReferenceException(nameof(moverToTarget));
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
