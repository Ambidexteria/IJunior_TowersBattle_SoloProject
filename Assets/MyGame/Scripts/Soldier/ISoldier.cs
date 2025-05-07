using System;
using UnityEngine;

public interface ISoldier : IDamageable, IMovable, IAttacker
{
    event Action<Transform> MovingToTarget;
    event Action<ISoldier> EnemyTargetDetected;
    event Action Dying;

    Transform GetTransform();
    TeamType GetTeam();
}
