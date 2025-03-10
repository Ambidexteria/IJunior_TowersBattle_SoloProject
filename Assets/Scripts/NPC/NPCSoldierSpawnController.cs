using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Team))]
public class NPCSoldierSpawnController : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private SoldierForDespawnDetector _despawnDetector;

    private Team _team;
    private WaitForSeconds _wait;
    private SoldierSpawner _spawner;
    private Soldier _soldier;
    private Coroutine _coroutine;

    public event Action<Soldier> Spawned;

    [Inject]
    private void Init(SoldierSpawner spawner)
    {
        _spawner = spawner;
        _wait = new WaitForSeconds(_spawnDelay);
        _team = GetComponent<Team>();

        _despawnDetector.Detected += _spawner.Despawn;
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator SpawnCoroutine()
    {
        while (enabled)
        {
            yield return _wait;

            _soldier = _spawner.Spawn();
            _soldier.transform.position = _spawnPoint.position;
            _soldier.SetTeam(_team);

            Spawned?.Invoke(_soldier);
        }
    }
}
