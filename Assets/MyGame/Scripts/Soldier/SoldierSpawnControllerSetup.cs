using Base.Infrastructure;
using UnityEngine;

namespace Base.Soldier
{
    public class SoldierSpawnControllerSetup : MonoBehaviour
    {
        [SerializeField] private SoldierSpawnControllerView _view;

        private SoldierSpawnControllerModel _model;
        private SoldierSpawnControllerPresenter _presenter;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(SoldierSpawnControllerSetup), nameof(CreateModel), _view);
        }

        public SoldierSpawnControllerModel CreateModel(float startDelay, float spawnDelay, float spawnRadius, Transform spawnPoint,
            SoldierForDespawnDetector despawnDetector, Team team, SoldierSpawner spawner,
            ICoroutineRunner coroutineRunner)
        {
            ExceptionsTest.NullRefMethodTest(nameof(SoldierSpawnControllerSetup), nameof(CreateModel),
                 spawnPoint, despawnDetector, team, spawner, coroutineRunner);

            _model = new( startDelay, spawnDelay, spawnRadius, spawnPoint, despawnDetector, team, spawner, coroutineRunner);

            _presenter = new SoldierSpawnControllerPresenter(_model, _view);
            _presenter.Enable();

            return _model;
        }

        private void OnDestroy()
        {
            _model = null;
        }
    }
}
