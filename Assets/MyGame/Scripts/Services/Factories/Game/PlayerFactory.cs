using Base.Data.Game;
using Base.GameLogic.Cannon;
using Base.GameLogic.ShootMinigame;
using Base.Infrastructure;
using Base.Services.AssetManagment;
using Base.Services.Input;
using Base.Services.TimeManagment;
using Base.Soldier;
using UnityEngine;
using Zenject;

namespace Base.Services.Factories.Game
{
    public class PlayerFactory : MonoBehaviour
    {
        private const string FloatingPointerAssetPath = "GameLogic/Soldier/FloatingPointer";

        [SerializeField] private SoldierForDespawnDetector _soldierDespawnDetector;
        [SerializeField] private CannonEnergyBarSetup _playerCannonEnergyBarSetup;
        [SerializeField] private SoldierSpawnControllerSetup _playerSpawnControllerSetup;
        [SerializeField] private RaycastSettings _soldierSelectorSettings;
        [SerializeField] private RaycastSettings _controlPointSelectorSettings;
        [SerializeField] private ShootMinigameSetup _shootMinigameSetup;

        private ICoroutineRunner _coroutineRunner;
        private AssetLoader _assetLoader;
        private SoldierSpawner _soldierSpawner;
        private CannonProjectileSpawner _projectileSpawner;
        private ControlPointDatabase _controlPointDatabase;
        private InputService _input;
        private TimeController _timeController;

        [Inject]
        private void Init(AssetLoader assetLoader, SoldierSpawner soldierSpawner, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner,
            ControlPointDatabase controlPointDatabase, InputService input, TimeController timeController)
        {
            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _soldierSpawner = soldierSpawner;
            _projectileSpawner = projectileSpawner;
            _controlPointDatabase = controlPointDatabase;
            _input = input;
            _timeController = timeController;
        }

        public Player CreatePlayer(Team team, CannonModel cannon, CannonData cannonData, float soldierSpawnDelay, Transform soldierSpawnPoint)
        {
            CannonEnergyBarModel cannonEnergyBar = _playerCannonEnergyBarSetup.CreateCannonEnergyBar(team,
                _controlPointDatabase, cannonData.MaxEnergy, _coroutineRunner);

            SoldierSpawnControllerModel spawnController = _playerSpawnControllerSetup.CreateSoldierSpawnController(
                soldierSpawnDelay, soldierSpawnPoint,
                _soldierDespawnDetector, team, _soldierSpawner, _coroutineRunner);

            ShootMinigameModel shootMinigame = _shootMinigameSetup.CreateShootMinigameModel(cannonEnergyBar,
                _timeController, _coroutineRunner);
            Player player = new (cannon, cannonEnergyBar, shootMinigame,
                spawnController, CreateSoldierCommandController(team));

            return player;
        }

        private SoldierCommandController CreateSoldierCommandController(Team team)
        {
            FloatingPointer floatingPointer = _assetLoader.Instantiate<FloatingPointer>(FloatingPointerAssetPath);
            SoldierSelector soldierSelector = new(_soldierSelectorSettings);
            ControlPointSelector controlPointSelector = new (_controlPointSelectorSettings);

            SoldierCommandController controller = new (0.1f, soldierSelector,
                controlPointSelector, floatingPointer, _coroutineRunner, team, _input);

            return controller;
        }
    }
}
