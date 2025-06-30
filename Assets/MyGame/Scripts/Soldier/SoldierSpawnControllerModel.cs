using Base.Infrastructure;
using Base.Soldier;
using System;
using System.Collections;
using UnityEngine;

public class SoldierSpawnControllerModel
{
    private readonly float _startSpawnDelay;
    private readonly float _spawnDelay;
    private readonly float _spawnRadius;
    private readonly Transform _spawnPoint;
    private readonly SoldierForDespawnDetector _despawnDetector;

    private readonly Team _team;
    private readonly WaitForSeconds _spawnCooldown;
    private readonly WaitForSeconds _startDelay;
    private readonly SoldierSpawner _spawner;
    private readonly SoldierSetup _soldierSetup;
    private readonly ICoroutineRunner _coroutineRunner;

    private Coroutine _spawnCoroutine;
    private Coroutine _countdownCoroutine;

    private bool _enabled = true;
    private float _nextSpawnTime = 0;

    public SoldierSpawnControllerModel(float startSpawnDelay, float spawnDelay, float spawnRadius, Transform spawnPoint, 
        SoldierForDespawnDetector despawnDetector, Team team, SoldierSpawner spawner,
        ICoroutineRunner coroutineRunner)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(SoldierSpawnControllerModel), spawnPoint,
            despawnDetector, team, spawner, coroutineRunner);

        _startSpawnDelay = startSpawnDelay;
        _spawnDelay = spawnDelay;
        _spawnRadius = spawnRadius;
        _spawnPoint = spawnPoint;
        _despawnDetector = despawnDetector;
        _team = team;
        _spawner = spawner;
        _coroutineRunner = coroutineRunner;
        _startDelay = new WaitForSeconds(_startSpawnDelay);
        _spawnCooldown = new WaitForSeconds(_spawnDelay);
    }

    public float TimeBeforeNextSpawn { get; private set; }

    public event Action<float> TimeBeforeNextSpawnChanged;
    public event Action<SoldierModel> Spawned;
    public event Action<SoldierModel> Despawned;

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
        if (_spawnCoroutine != null)
            return;

        _spawnCoroutine = _coroutineRunner.LaunchCoroutine(SpawnCoroutine());
        _countdownCoroutine = _coroutineRunner.LaunchCoroutine(SpawnCountdown());
    }

    private void StopSpawn()
    {
        if (_spawnCoroutine != null)
        {
            _coroutineRunner.EndCoroutine(_spawnCoroutine);
            _coroutineRunner.EndCoroutine(_countdownCoroutine);
        }
    }

    private IEnumerator SpawnCountdown()
    {
        while(_enabled)
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
        ExceptionsTest.NullRefMethodTest(nameof(SoldierSpawnControllerModel), nameof(OnSoldierDespawning), setup);

        SoldierModel soldier = setup.GetSoldier();
        soldier.DespawnerDetected -= OnSoldierDespawning;
        soldier.Disable();
        Despawned?.Invoke(soldier);
        _spawner.Despawn(setup);
    }
}
