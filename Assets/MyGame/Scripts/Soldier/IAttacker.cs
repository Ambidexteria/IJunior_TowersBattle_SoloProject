public interface IAttacker
{
    void Attack(ITargetSoldier soldier);

    void StopAttack();

    bool TryGetNextAttackTarget(out ITargetSoldier target);
}
