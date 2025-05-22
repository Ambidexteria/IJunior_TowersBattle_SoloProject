using Base.Infrastructure;
using UnityEngine;

namespace Base.Soldier
{
    public class SoldierSpawnControllerSetup : MonoBehaviour
    {
        [SerializeField] private SoldierSpawnControllerView _view;

        private SoldierSpawnControllerModel _model;
        private SoldierSpawnControllerPresenter _presenter;

        public SoldierSpawnControllerModel CreateSoldierSpawnController(float spawnDelay, Transform spawnPoint, 
            SoldierForDespawnDetector despawnDetector, Team team, SoldierSpawner spawner, 
            ICoroutineRunner coroutineRunner)
        {
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
