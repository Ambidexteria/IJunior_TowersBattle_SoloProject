using UnityEngine;

public class AttackSoldierState : ISoldierState
{
    private IDamageable _attackTarget;
    private Animator _animator;
    private Soldier _soldier;

    public AttackSoldierState(Animator animator, Soldier soldier)
    {
        _animator = animator;
        _soldier = soldier;
    }

    public void OnStart(SoldierStateContext context)
    {
        _attackTarget = context.AttackTarget;
        
        _animator.SetTrigger(SoldierAnimationTriggerNames.IdleToAttack);
        _soldier.Attack(_attackTarget);
    }

    public void OnStop()
    {
        _animator.SetTrigger(SoldierAnimationTriggerNames.AttackToIdle);
        _soldier.StopAttack();
    }

    public void OnUpdate()
    {
    }
}
