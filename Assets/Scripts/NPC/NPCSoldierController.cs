using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Team))]
public class NPCSoldierController : MonoBehaviour
{
    [SerializeField] private ControlPointDatabase _controlPointDatabase;
    [SerializeField] private List<Soldier> _soldiers = new List<Soldier>();
    [SerializeField] private SoldierSpawnController _spawnController;
    [SerializeField] private float _startDelay = 1f;
    [SerializeField] private float _nextCommandDelay = 2f;

    private Team _team;
    private Coroutine _coroutine;
    private WaitForSeconds _startWait;
    private WaitForSeconds _waitNextCommand;

    private void Awake()
    {
        _team = GetComponent<Team>();
        _startWait = new WaitForSeconds(_startDelay);
        _waitNextCommand = new WaitForSeconds(_nextCommandDelay);
    }

    private void Start()
    {
        //InvokeRepeating(nameof(SendSoldierToControlPoint), _startDelay, _nextCommandDelay);

        LaunchSendingSoldiers();
    }

    private void OnEnable()
    {
        _spawnController.Spawned += AddNewSoldier;
        _spawnController.Despawned += RemoveSoldier;
    }

    private void OnDisable()
    {
        _spawnController.Spawned -= AddNewSoldier;
        _spawnController.Despawned -= RemoveSoldier;
    }

    public void LaunchSendingSoldiers()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(SendSoldierToControlPointCoroutine());
    }

    public void StopSendingSoldiers()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator SendSoldierToControlPointCoroutine()
    {
        yield return _startWait;

        while (enabled)
        {
            if (_soldiers.Count > 0)
                if (TryGetIdleSoldier(out Soldier soldier))
                    if (_controlPointDatabase.TryGetNearestVacantControlPoint(_team.Type, soldier.transform.position, out var controlPoint))
                        soldier.MoveTo(controlPoint.transform);

            yield return _waitNextCommand;
        }
    }

    private void AddNewSoldier(Soldier soldier)
    {
        if (soldier.GetTeam() != _team.Type)
            Debug.LogError("Trying add soldier from different team");

        _soldiers.Add(soldier);
    }

    private void RemoveSoldier(Soldier soldier)
    {
        if (soldier.GetTeam() != _team.Type)
            Debug.LogError("Trying add soldier from different team");

        if (_soldiers.Remove(soldier) == false)
            Debug.LogError($"Cannot remove {soldier.gameObject.name} from List");
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
