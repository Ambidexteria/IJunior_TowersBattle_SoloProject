using System;
using UnityEngine;

public class AttackSoldierState : ISoldierState
{
    private readonly Animator _animator;
    private readonly IAttacker _soldier;

    private ISoldier _attackTarget;

    public AttackSoldierState(Animator animator, IAttacker soldier, IMovable movable)
    {
        _animator = animator;
        _soldier = soldier;
    }

    public event Action AllTargetsDestroyed;

    public void OnStart(SoldierStateContext context)
    {
        _attackTarget = context.AttackTarget;
        _soldier.Attack(_attackTarget);
        _animator.SetTrigger(SoldierAnimationTriggerNames.IdleToAttack);
    }

    public void OnStop()
    {
        _animator.SetTrigger(SoldierAnimationTriggerNames.AttackToIdle);
        _soldier.StopAttack();
    }

    public void OnUpdate()
    {
        if (_attackTarget.IsDead())
        {
            if (_soldier.TryGetNextAttackTarget(out ISoldier nextTarget))
            {
                _attackTarget = nextTarget;
                _soldier.Attack(nextTarget);
            }
            else
            {
                AllTargetsDestroyed?.Invoke();
            }
        }
    }
}
