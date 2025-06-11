using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Base.Infrastructure;

public class NPCSoldierController
{
    [SerializeField] private ControlPointDatabase _controlPointDatabase;
    [SerializeField] private List<SoldierModel> _soldiers = new List<SoldierModel>();
    [SerializeField] private SoldierSpawnControllerModel _spawnController;
    [SerializeField] private float _startDelay = 1f;
    [SerializeField] private float _nextCommandDelay = 2f;

    private Team _team;
    private readonly ICoroutineRunner _coroutineRunner;
    private Coroutine _coroutine;
    private WaitForSeconds _startWait;
    private WaitForSeconds _waitNextCommand;
    private bool _enabled = true;

    public NPCSoldierController(ControlPointDatabase controlPointDatabase, SoldierSpawnControllerModel spawnController,
        float startDelay, float nextCommandDelay, Team team, ICoroutineRunner coroutineRunner)
    {
        _controlPointDatabase = controlPointDatabase;
        _spawnController = spawnController;
        _startDelay = startDelay;
        _nextCommandDelay = nextCommandDelay;
        _team = team;
        _coroutineRunner = coroutineRunner;

        _startWait = new WaitForSeconds(_startDelay);
        _waitNextCommand = new WaitForSeconds(_nextCommandDelay);
    }

    public void Enable()
    {
        LaunchSendingSoldiers();
        _spawnController.Spawned += AddNewSoldier;
        _spawnController.Despawned += RemoveSoldier;
    }

    public void Disable()
    {
        StopSendingSoldiers();

        _spawnController.Spawned -= AddNewSoldier;
        _spawnController.Despawned -= RemoveSoldier;
    }

    private void LaunchSendingSoldiers()
    {
        if (_coroutine != null)
            return;

        _coroutine = _coroutineRunner. LaunchCoroutine(SendSoldierToControlPointCoroutine());
    }

    private  void StopSendingSoldiers()
    {
        if (_coroutine != null)
            _coroutineRunner.EndCoroutine(_coroutine);
    }

    private IEnumerator SendSoldierToControlPointCoroutine()
    {
        yield return _startWait;

        while (_enabled)
        {
            if (_soldiers.Count > 0)
                if (TryGetIdleSoldier(out SoldierModel soldier))
                    if (_controlPointDatabase.TryGetNearestVacantControlPoint(_team.Type, soldier.GetTransform().position, out var controlPoint))
                        soldier.MoveTo(controlPoint.transform);

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
        if (_soldiers.Count == 0)
            return;

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
