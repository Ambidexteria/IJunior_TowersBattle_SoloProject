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

        Soldier soldier = _soldiers[Random.Range(0, _soldiers.Count)];

        if (_controlPointDatabase. TryGetNearestVacantControlPoint(_team.Type, soldier.transform.position, out var controlPoint))
        {
            soldier.MoveTo(controlPoint.transform);
            Debug.Log("Soldier has been sended");
        }
        else
        {
            Debug.LogError("Cannot Send Soldier");
        }
    }
}
