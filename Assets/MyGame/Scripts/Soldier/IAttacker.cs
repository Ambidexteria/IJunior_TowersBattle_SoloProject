public interface IAttacker
{
    void Attack(ISoldier soldier);

    void StopAttack();

    bool TryGetNextAttackTarget(out ISoldier target);
}
