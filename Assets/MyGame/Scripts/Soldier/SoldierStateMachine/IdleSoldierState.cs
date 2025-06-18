using System;
using UnityEngine;

public class IdleSoldierState : ISoldierState
{
    private Animator _animator;

    public IdleSoldierState(Animator animator)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(IdleSoldierState), animator);

        _animator = animator;
    }

    public void OnStart(SoldierStateContext context)
    {
        ExceptionsTest.NullRefMethodTest(nameof(IdleSoldierState), nameof(OnStart), context);
    }

    public void OnStop()
    {
    }

    public void OnUpdate()
    {
    }
}
