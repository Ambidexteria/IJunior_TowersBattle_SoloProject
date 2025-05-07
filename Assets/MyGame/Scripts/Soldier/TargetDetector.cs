using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Base.Logic;

public class TargetDetector
{
    private const int NumberOfSoldiersToCallDetectedEvent = 1;

    private TriggerObserver _triggerObserver;
    private Team _team;
    private List<ISoldier> _enemySoldiers = new List<ISoldier>();

    public TargetDetector(TriggerObserver triggerObserver)
    {
        _triggerObserver = triggerObserver;
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

    public void SetTeam(Team team)
    {
        _team = team;
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
        if (other.TryGetComponent(out ISoldier target))
            if (IsTargetAliveEnemy(target))
                if (_enemySoldiers.Contains(target) == false)
                    _enemySoldiers.Add(target);

        DeleteDeadEnemies();

        if (_enemySoldiers.Count == NumberOfSoldiersToCallDetectedEvent)
        {
            Detected?.Invoke(target);
        }
    }

    private void OnTriggerExited(Collider other)
    {
        if (other.TryGetComponent(out ISoldier target))
            if (_enemySoldiers.Contains(target))
                _enemySoldiers.Remove(target);
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
