using System;
using UnityEngine;

public class DieSoldierState : ISoldierState
{
    private Animator _animator;

    public DieSoldierState(Animator animator)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(DieSoldierState), animator);

        _animator = animator;
    }

    public event Action Dying;
    //public event Action Dead;

    public void OnStart(SoldierStateContext context)
    {
        ExceptionsTest.NullRefMethodTest(nameof(DieSoldierState), nameof(OnStart), context);

        _animator.SetTrigger(SoldierAnimationTriggerNames.Death);
        Dying?.Invoke();
    }

    public void OnStop()
    {
    }

    public void OnUpdate()
    {
    }
}
