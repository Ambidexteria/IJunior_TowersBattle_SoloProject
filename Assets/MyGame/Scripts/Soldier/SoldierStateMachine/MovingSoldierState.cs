using System;
using UnityEngine;

public class MovingSoldierState : ISoldierState
{
    private Animator _animator;
    private IMovable _movable;

    public MovingSoldierState(Animator animator, IMovable moverToTarget)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(MovingSoldierState), animator, moverToTarget);

        _animator = animator;
        _movable = moverToTarget;
    }

    public event Action TargetReached;

    public void OnStart(SoldierStateContext context)
    {
        ExceptionsTest.NullRefMethodTest(nameof(MovingSoldierState), nameof(OnStart), context);

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
