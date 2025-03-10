using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TargetDetector : MonoBehaviour
{
    private const int NumberOfSoldiersToCallDetectedEvent = 1;

    private Team _team;
    private List<ITargetSoldier> _enemySoldiers = new List<ITargetSoldier>();

    public event Action<ITargetSoldier> Detected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITargetSoldier target))
            if (IsTargetAliveEnemy(target))
                if (_enemySoldiers.Contains(target) == false)
                    _enemySoldiers.Add(target);

        DeleteDeadEnemies();

        if (_enemySoldiers.Count == NumberOfSoldiersToCallDetectedEvent)
            Detected?.Invoke(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ITargetSoldier target))
            if (_enemySoldiers.Contains(target))
                _enemySoldiers.Remove(target);
    }

    public void SetTeam(Team team)
    {
        _team = team;
    }

    public bool TryGetNextAttackTarget(out ITargetSoldier target)
    {
        DeleteDeadEnemies();

        target = null;
        List<ITargetSoldier> targets = _enemySoldiers.Where(x => x.IsDead() == false).ToList();

        if (targets.Count > 0)
        {
            target = targets[0];
            return true;
        }

        return false;
    }

    private bool IsTargetAliveEnemy(ITargetSoldier target)
    {
        return target.GetTeam() != _team.Type && target.IsDead() == false;
    }

    private void DeleteDeadEnemies()
    {
        _enemySoldiers = _enemySoldiers.Where(x => x.IsDead() == false).ToList();
    }
}
