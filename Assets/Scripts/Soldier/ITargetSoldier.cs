using UnityEngine;

public interface ITargetSoldier : IDamageable
{
    Transform GetTransform();
    Team GetTeam();
}
