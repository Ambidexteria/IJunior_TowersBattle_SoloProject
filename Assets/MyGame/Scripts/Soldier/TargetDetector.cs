using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Base.Logic;
using Base.Soldier;

public class TargetDetector
{
    private const int NumberOfSoldiersToCallDetectedEvent = 1;

    private TriggerObserver _triggerObserver;
    private Team _team;
    private List<ISoldier> _enemySoldiers = new List<ISoldier>();

    public TargetDetector(TriggerObserver triggerObserver, Team team)
    {
        _triggerObserver = triggerObserver;
        _team = team;
    }

    public event Action<ISoldier> Detected;

    public void Enable()
    {
        _enemySoldiers.Clear();
        _triggerObserver.Entered += OnTriggerEntered;
        _triggerObserver.Exited += OnTriggerExited;
    }

    public void Disable()
    {
        _triggerObserver.Entered -= OnTriggerEntered;
        _triggerObserver.Exited -= OnTriggerExited;
    }

    public bool TryGetNextAttackTarget(out ISoldier target)
    {
        DeleteDeadEnemies();

        target = null;
        List<ISoldier> targets = _enemySoldiers.Where(x => x.IsDead() == false).ToList();

        if (targets.Count > 0)
        {
            target = targets[0];
            return true;
        }

        return false;
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out SoldierSetup target))
            if (IsTargetAliveEnemy(target.GetSoldier()))
                if (_enemySoldiers.Contains(target.GetSoldier()) == false)
                    _enemySoldiers.Add(target.GetSoldier());

        DeleteDeadEnemies();

        if (_enemySoldiers.Count == NumberOfSoldiersToCallDetectedEvent)
            if (target != null)
                Detected?.Invoke(target.GetSoldier());
    }

    private void OnTriggerExited(Collider other)
    {
        if (other.TryGetComponent(out SoldierSetup target))
            if (_enemySoldiers.Contains(target.GetSoldier()))
                _enemySoldiers.Remove(target.GetSoldier());
    }

    private bool IsTargetAliveEnemy(ISoldier target)
    {
        return target.GetTeam() != _team.Type && target.IsDead() == false;
    }

    private void DeleteDeadEnemies()
    {
        _enemySoldiers = _enemySoldiers.Where(x => x.IsDead() == false).ToList();
    }
}
