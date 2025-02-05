using System.Collections;
using UnityEngine;

public class SoldierSpawner : GenericSpawner<Soldier>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnCooldown = 3f;
    [SerializeField] private int _solidersMaxCount = 5;

    private int _currentSoldiersCount;
    private WaitForSeconds _sleep;
    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _sleep = new WaitForSeconds(_spawnCooldown);
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (_solidersMaxCount > _currentSoldiersCount)
        {
            yield return _sleep;

            Soldier soldier = Spawn();

            if (_spawnPoint != null)
                soldier.transform.position = _spawnPoint.position;
            else
                Debug.LogError("SpawnPoint doesn't assigned");
        }
    }
}
