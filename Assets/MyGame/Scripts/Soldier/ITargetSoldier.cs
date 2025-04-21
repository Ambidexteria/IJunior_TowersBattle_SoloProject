using UnityEngine;

public interface ITargetSoldier : IDamageable
{
    Transform GetTransform();
    TeamType GetTeam();
}
