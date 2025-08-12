using System;
using UnityEngine;

public class DieSoldierState : ISoldierState
{
    private readonly Animator _animator;

    public DieSoldierState(Animator animator)
    {
        _animator = animator;
    }

    public event Action Dying;

    public void OnStart(SoldierStateContext context)
    {
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
