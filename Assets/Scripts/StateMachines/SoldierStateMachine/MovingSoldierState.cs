using System;
using UnityEngine;

public class MovingSoldierState : ISoldierState
{
    private Animator _animator;
    private IMovable _soldier;

    public MovingSoldierState(Animator animator, IMovable moverToTarget)
    {
        _animator = animator ?? throw new NullReferenceException(nameof(animator));
        _soldier = moverToTarget ?? throw new NullReferenceException(nameof(moverToTarget));
    }

    public event Action TargetReached;

    public void OnStart(SoldierStateContext context)
    {
        _animator.SetTrigger(SoldierAnimationTriggerNames.IdleToMove);

        if (context.MoveTarget != null)
            _soldier.MoveTo(context.MoveTarget);
    }

    public void OnStop()
    {
        _soldier.Stop();

        _animator.SetTrigger(SoldierAnimationTriggerNames.MoveToIdle);
    }

    public void OnUpdate()
    {
        if(_soldier.TargetReached())
            TargetReached?.Invoke();
    }
}
