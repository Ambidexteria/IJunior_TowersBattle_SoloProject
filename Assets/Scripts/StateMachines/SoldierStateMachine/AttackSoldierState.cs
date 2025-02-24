using System;
using UnityEngine;

public class AttackSoldierState : ISoldierState
{
    private ITargetSoldier _attackTarget;
    private Animator _animator;
    private Soldier _soldier;

    public AttackSoldierState(Animator animator, Soldier soldier)
    {
        _animator = animator;
        _soldier = soldier;
    }

    public event Action TargetDestroyed;

    public void OnStart(SoldierStateContext context)
    {
        _attackTarget = context.AttackTarget;
        
        _animator.SetTrigger(SoldierAnimationTriggerNames.IdleToAttack);
    }

    public void OnStop()
    {
        _animator.SetTrigger(SoldierAnimationTriggerNames.AttackToIdle);
        _soldier.StopAttack();
    }

    public void OnUpdate()
    {
        if(_attackTarget.IsDead())
            TargetDestroyed?.Invoke();
    }
}
