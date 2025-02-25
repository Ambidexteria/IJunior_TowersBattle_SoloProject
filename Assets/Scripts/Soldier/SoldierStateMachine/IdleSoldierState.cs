using System;
using UnityEngine;

public class IdleSoldierState : ISoldierState
{
    private Animator _animator;

    public IdleSoldierState(Animator animator)
    {
        _animator = animator ?? throw new NullReferenceException(nameof(animator));
    }

    public void OnStart(SoldierStateContext context)
    {
    }

    public void OnStop()
    {
    }

    public void OnUpdate()
    {
    }
}
