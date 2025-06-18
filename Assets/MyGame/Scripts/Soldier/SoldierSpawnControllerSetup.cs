using Base.Infrastructure;
using Unity.VisualScripting;
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
            ExceptionsTest.NullRefMethodTest(nameof(SoldierSpawnControllerSetup), nameof(CreateSoldierSpawnController), _view);
        }

        public SoldierSpawnControllerModel CreateSoldierSpawnController(float spawnDelay, Transform spawnPoint,
            SoldierForDespawnDetector despawnDetector, Team team, SoldierSpawner spawner,
            ICoroutineRunner coroutineRunner)
        {
            ExceptionsTest.NullRefMethodTest(nameof(SoldierSpawnControllerSetup), nameof(CreateSoldierSpawnController),
                 spawnPoint, despawnDetector, team, spawner, coroutineRunner);

            _model = new(spawnDelay, spawnPoint, despawnDetector, team, spawner, coroutineRunner);

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
