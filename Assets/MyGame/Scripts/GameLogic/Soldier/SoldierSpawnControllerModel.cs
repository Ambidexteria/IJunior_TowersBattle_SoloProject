using System;
using System.Collections;
using UnityEngine;
using Base.Infrastructure;
using Base.Soldier;

public class SoldierSpawnControllerModel
{
    private readonly float _startSpawnDelay;
    private readonly float _spawnDelay;
    private readonly float _spawnRadius;
    private readonly Transform _spawnPoint;
    private readonly SoldierForDespawnDetector _despawnDetector;

    private readonly WaitForSeconds _spawnCooldown;
    private readonly WaitForSeconds _startDelay;
    private readonly SoldierSpawner _spawner;
    private readonly ICoroutineRunner _coroutineRunner;

    private Coroutine _spawnCoroutine;
    private Coroutine _countdownCoroutine;

    private bool _enabled = false;
    private float _nextSpawnTime = 0;

    public SoldierSpawnControllerModel(
        float startSpawnDelay, 
        float spawnDelay, 
        float spawnRadius, 
        Transform spawnPoint,
        SoldierForDespawnDetector despawnDetector, 
        SoldierSpawner spawner,
        ICoroutineRunner coroutineRunner)
    {
        _startSpawnDelay = startSpawnDelay;
        _spawnDelay = spawnDelay;
        _spawnRadius = spawnRadius;
        _spawnPoint = spawnPoint;
        _despawnDetector = despawnDetector;
        _spawner = spawner;
        _coroutineRunner = coroutineRunner;

        _startDelay = new WaitForSeconds(_startSpawnDelay);
        _spawnCooldown = new WaitForSeconds(_spawnDelay);
    }

    public event Action<float> TimeBeforeNextSpawnChanged;
    public event Action<SoldierModel> Spawned;
    public event Action<SoldierModel> Despawned;

    public float TimeBeforeNextSpawn { get; private set; }

    public void Enable()
    {
        if (_enabled)
            return;

        _enabled = true;

        StartSpawn();
    }

    public void Disable()
    {
        if (_enabled == false)
            return;

        _enabled = false;

        StopSpawn();
    }

    private void StartSpawn()
    {
        if (_spawnCoroutine != null)
            return;

        _spawnCoroutine = _coroutineRunner.LaunchCoroutine(SpawnCoroutine());
        _countdownCoroutine = _coroutineRunner.LaunchCoroutine(CountdownCoroutine());
    }

    private void StopSpawn()
    {
        if (_spawnCoroutine != null)
        {
            _coroutineRunner.EndCoroutine(_spawnCoroutine);
            _coroutineRunner.EndCoroutine(_countdownCoroutine);
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        while (_enabled)
        {
            TimeBeforeNextSpawn = Mathf.Clamp(_nextSpawnTime - Time.time, 0, _spawnDelay);

            TimeBeforeNextSpawnChanged?.Invoke(TimeBeforeNextSpawn);
            yield return null;
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        _nextSpawnTime = Time.time + _startSpawnDelay;
        yield return _startDelay;

        while (_enabled)
        {
            SoldierSetup setup = _spawner.Spawn();
            SoldierModel soldier = setup.GetSoldier();

            setup.gameObject.SetActive(true);

            soldier.GetTransform().position = GetRandomSpawnPosition();
            soldier.Enable();
            soldier.DespawnerDetected += OnSoldierDespawning;

            Spawned?.Invoke(soldier);

            _nextSpawnTime = Time.time + _spawnDelay;

            yield return _spawnCooldown;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = _spawnPoint.position;
        Vector2 random = UnityEngine.Random.insideUnitCircle * _spawnRadius;

        return new Vector3(spawnPosition.x + random.x, spawnPosition.y, spawnPosition.z + random.y);
    }

    private void OnSoldierDespawning(SoldierSetup setup)
    {
        SoldierModel soldier = setup.GetSoldier();
        soldier.DespawnerDetected -= OnSoldierDespawning;
        soldier.Disable();
        Despawned?.Invoke(soldier);
        _spawner.Despawn(setup);
    }
}
