using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Team))]
public class NPCSoldierController : MonoBehaviour
{
    [SerializeField] private ControlPointDatabase _controlPointDatabase;
    [SerializeField] private List<Soldier> _soldiers = new List<Soldier>();
    [SerializeField] private NPCSoldierSpawnController _spawnController;
    [SerializeField] private float _nextCommandDelay = 2f;

    private Team _team;

    private void Awake()
    {
        _team = GetComponent<Team>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(SendSoldierToControlPoint), 0f, _nextCommandDelay);
    }

    private void OnEnable()
    {
        _spawnController.Spawned += AddNewSoldier;
    }

    private void OnDisable()
    {
        _spawnController.Spawned -= AddNewSoldier;
    }

    private void AddNewSoldier(Soldier soldier)
    {
        if (soldier.GetTeam() != _team.Type)
            Debug.LogError("Trying add soldier from different team");

        _soldiers.Add(soldier);
    }

    private void SendSoldierToControlPoint()
    {
        if (_soldiers.Count == 0)
            return;

        if (TryGetIdleSoldier(out Soldier soldier))
            if (_controlPointDatabase.TryGetNearestVacantControlPoint(_team.Type, soldier.transform.position, out var controlPoint))
                soldier.MoveTo(controlPoint.transform);
    }

    private bool TryGetIdleSoldier(out Soldier soldier)
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
