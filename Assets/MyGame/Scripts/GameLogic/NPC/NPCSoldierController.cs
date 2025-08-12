using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Base.Infrastructure;

public class NPCSoldierController
{
    private readonly ControlPointDatabase _controlPointDatabase;
    private readonly List<SoldierModel> _soldiers;
    private readonly SoldierSpawnControllerModel _spawnController;
    private readonly float _startDelay = 1f;
    private readonly float _nextCommandDelay = 2f;
    private readonly int _commandCount = 2;
    private readonly Team _team;
    private readonly ICoroutineRunner _coroutineRunner;

    private Coroutine _coroutine;
    private WaitForSeconds _startCommandDelay;
    private WaitForSeconds _waitNextCommand;
    private bool _enabled = false;

    public NPCSoldierController(ControlPointDatabase controlPointDatabase, SoldierSpawnControllerModel spawnController,
        float startCommandDelay, float nextCommandDelay, Team team, ICoroutineRunner coroutineRunner)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(NPCSoldierController), controlPointDatabase, spawnController,
            team, coroutineRunner);

        _soldiers = new();
        _controlPointDatabase = controlPointDatabase;
        _spawnController = spawnController;
        _startDelay = startCommandDelay;
        _nextCommandDelay = nextCommandDelay;
        _team = team;
        _coroutineRunner = coroutineRunner;

        _startCommandDelay = new WaitForSeconds(_startDelay);
        _waitNextCommand = new WaitForSeconds(_nextCommandDelay);
    }

    public void Enable()
    {
        if (_enabled)
            return;

        _enabled = true;

        LaunchSendingSoldiers();
        _spawnController.Spawned += AddNewSoldier;
        _spawnController.Despawned += RemoveSoldier;
    }

    public void Disable()
    {
        if(_enabled == false)
            return;

        _enabled = false;

        StopSendingSoldiers();

        _spawnController.Spawned -= AddNewSoldier;
        _spawnController.Despawned -= RemoveSoldier;
    }

    private void LaunchSendingSoldiers()
    {
        if (_coroutine != null)
            return;

        _coroutine = _coroutineRunner.LaunchCoroutine(SendSoldierToControlPointCoroutine());
    }

    private  void StopSendingSoldiers()
    {
        if (_coroutine != null)
            _coroutineRunner.EndCoroutine(_coroutine);
    }

    private IEnumerator SendSoldierToControlPointCoroutine()
    {
        yield return _startCommandDelay;

        while (_enabled)
        {
            for (int i = 0; i < _commandCount; i++) 
            {
                SendSoldierToControlPoint();
            }

            yield return _waitNextCommand;
        }
    }

    private void AddNewSoldier(SoldierModel soldier)
    {
        if (soldier.GetTeam() != _team.Type)
            Debug.LogError("Trying add soldier from different team");

        _soldiers.Add(soldier);
    }

    private void RemoveSoldier(SoldierModel soldier)
    {
        if (soldier.GetTeam() != _team.Type)
            Debug.LogError("Trying add soldier from different team");

        if (_soldiers.Remove(soldier) == false)
            Debug.LogError($"Cannot remove {soldier.GetTransform().name} from List");
    }

    private void SendSoldierToControlPoint()
    {
        if (_soldiers.Count > 0)
            if (TryGetIdleSoldier(out SoldierModel soldier))
                if (_controlPointDatabase.TryGetNearestVacantControlPoint(_team.Type, soldier.GetTransform().position, out var controlPoint))
                    soldier.MoveTo(controlPoint.transform);
    }

    private bool TryGetIdleSoldier(out SoldierModel soldier)
    {
        soldier = null;
        var idleSoldiers = _soldiers.Where(x => x.IsDead() == false).Where(x => x.IsIdle).ToList();

        if (idleSoldiers.Count > 0)
        {
            soldier = idleSoldiers[Random.Range(0, idleSoldiers.Count)];
            return true;
        }

        return false;
    }
}
