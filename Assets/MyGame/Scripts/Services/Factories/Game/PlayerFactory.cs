using Base.Data.Player;
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
        private const string PlayerCannon = "GameLogic/Cannon/PlayerCannon Variant";
        private const string FloatingPointer = "GameLogic/Soldier/FloatingPointer";

        [SerializeField] private SoldierForDespawnDetector _soldierDespawnDetector;
        [SerializeField] private Transform _playerSpawnPoint;
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

        public Player CreatePlayer(Team team, CannonModel cannon, CannonData cannonData, float soldierSpawnDelay)
        {
            CannonEnergyBarModel cannonEnergyBar = _playerCannonEnergyBarSetup.CreateCannonEnergyBar(team,
                _controlPointDatabase, cannonData.MaxEnergy, _coroutineRunner);

            SoldierSpawnControllerModel spawnController = _playerSpawnControllerSetup.CreateSoldierSpawnController(soldierSpawnDelay, _playerSpawnPoint,
                _soldierDespawnDetector, team, _soldierSpawner, _coroutineRunner);

            ShootMinigameModel shootMinigame = _shootMinigameSetup.CreateShootMinigameModel(cannonEnergyBar,
                _timeController, _coroutineRunner);
            Player player = new Player(cannon, cannonEnergyBar, shootMinigame,
                spawnController, CreateSoldierCommandController(team));

            return player;
        }

        private SoldierCommandController CreateSoldierCommandController(Team team)
        {
            FloatingPointer floatingPointer = _assetLoader.Instantiate<FloatingPointer>(FloatingPointer);
            SoldierSelector soldierSelector = new(_soldierSelectorSettings);
            ControlPointSelector controlPointSelector = new ControlPointSelector(_controlPointSelectorSettings);

            SoldierCommandController controller = new SoldierCommandController(0.1f, soldierSelector,
                controlPointSelector, floatingPointer, _coroutineRunner, team, _input);

            return controller;
        }
    }
}
