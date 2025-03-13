using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Team))]
public class SoldierSpawnController : MonoBehaviour
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
    public event Action<Soldier> Despawned;

    [Inject]
    private void Init(SoldierSpawner spawner)
    {
        _spawner = spawner;
        _wait = new WaitForSeconds(_spawnDelay);
        _team = GetComponent<Team>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnEnable()
    {
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
            _soldier = _spawner.Spawn();
            _soldier.transform.position = _spawnPoint.position;
            _soldier.SetTeam(_team);
            _soldier.gameObject.SetActive(true);
            _soldier.DespawnerDetected += OnSoldierDespawning;

            Spawned?.Invoke(_soldier);

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
