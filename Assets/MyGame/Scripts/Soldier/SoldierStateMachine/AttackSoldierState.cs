using System;
using UnityEngine;

public class AttackSoldierState : ISoldierState
{
    private ITargetSoldier _attackTarget;
    private Animator _animator;
    private IAttacker _soldier;

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
            if (_soldier.TryGetNextAttackTarget(out ITargetSoldier nextTarget))
            {
                if (nextTarget == null)
                    Debug.Log("OnUpdate AttackState target == null");

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
