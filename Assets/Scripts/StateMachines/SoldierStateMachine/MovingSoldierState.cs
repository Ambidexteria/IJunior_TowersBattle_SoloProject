using System;
using UnityEngine;

public class MovingSoldierState : ISoldierState
{
    private Animator _animator;
    private SoldierMoverToTarget _moverToTarget;

    public MovingSoldierState(Animator animator, SoldierMoverToTarget moverToTarget)
    {
        _animator = animator ?? throw new NullReferenceException(nameof(animator));
        _moverToTarget = moverToTarget ?? throw new NullReferenceException(nameof(moverToTarget));
    }

    public event Action TargetReached;

    public void OnStart(SoldierStateContext context)
    {
        //_animator.SetTrigger(SoldierAnimationTriggerNames.IdleToMove);

        _animator.SetTrigger(SoldierAnimationTriggerNames.IdleToMove);

        if (context.MoveTarget != null)
            _moverToTarget.MoveTo(context.MoveTarget);
    }

    public void OnStop()
    {
        _moverToTarget.Stop();
    }

    public void OnUpdate()
    {
        if(_moverToTarget.TargetReached())
            TargetReached?.Invoke();
    }
}
