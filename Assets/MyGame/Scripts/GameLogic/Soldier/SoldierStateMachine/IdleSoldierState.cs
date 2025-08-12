using UnityEngine;

public class IdleSoldierState : ISoldierState
{
    private Animator _animator;

    public IdleSoldierState(Animator animator)
    {
        _animator = animator;
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
