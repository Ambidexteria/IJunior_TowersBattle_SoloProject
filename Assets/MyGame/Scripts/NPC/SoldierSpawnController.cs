using Base.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

public class SoldierSpawnController
{
    private float _spawnDelay;
    private Transform _spawnPoint;
    private SoldierForDespawnDetector _despawnDetector;

    private Team _team;
    private WaitForSeconds _wait;
    private SoldierSpawner _spawner;
    private Soldier _soldier;
    private Coroutine _coroutine;

    private bool _enabled = true;
    private float _nextSpawnTime = 0;
    private ICoroutineRunner _coroutineRunner;

    public SoldierSpawnController(float spawnDelay, Transform spawnPoint, 
        SoldierForDespawnDetector despawnDetector, Team team, SoldierSpawner spawner,
        ICoroutineRunner coroutineRunner)
    {
        _spawnDelay = spawnDelay;
        _spawnPoint = spawnPoint;
        _despawnDetector = despawnDetector;
        _team = team;
        _spawner = spawner;
        _coroutineRunner = coroutineRunner;

        _wait = new WaitForSeconds(_spawnDelay);
    }

    public float TimeBeforeNextSpawn => Mathf.Clamp(_nextSpawnTime - Time.time, 0, _spawnDelay);

    public event Action<Soldier> Spawned;
    public event Action<Soldier> Despawned;

    public void Enable()
    {
        StartSpawn();
    }

    public void Disable()
    {
        StopSpawn();
    }

    private void StartSpawn()
    {
        if (_coroutine != null)
            return;

        _coroutine = _coroutineRunner.StartCoroutine(SpawnCoroutine());
    }

    private void StopSpawn()
    {
        if (_coroutine != null)
            _coroutineRunner.StopCoroutine(_coroutine);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (_enabled)
        {
            _soldier = _spawner.Spawn();
            _soldier.transform.position = _spawnPoint.position;
            _soldier.SetTeam(_team);
            _soldier.gameObject.SetActive(true);
            _soldier.DespawnerDetected += OnSoldierDespawning;

            Spawned?.Invoke(_soldier);

            _nextSpawnTime = Time.time + _spawnDelay;

            yield return _wait;
        }
    }

    private void OnSoldierDespawning(Soldier soldier)
    {
        soldier.DespawnerDetected -= OnSoldierDespawning;
        Despawned?.Invoke(soldier);
        _spawner.Despawn(soldier);
    }
}
