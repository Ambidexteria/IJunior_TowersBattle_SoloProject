using Base.Data.Game;
using Base.GameLogic.Cannon;
using Base.GameLogic.ShootMinigame;
using Base.Infrastructure;
using Base.Services.AssetManagment;
using Base.Services.TimeManagment;
using Base.Soldier;
using UnityEngine;
using Zenject;

namespace Base.Services.Factories.Game
{
    public class PlayerFactory : MonoBehaviour
    {
        private const string FloatingPointerAssetPath = "GameLogic/Soldier/FloatingPointer";

        [SerializeField] private SpawnerSettings _soldierSpawnerSettings;
        [SerializeField] private SoldierForDespawnDetector _soldierDespawnDetector;
        [SerializeField] private CannonEnergyBarSetup _playerCannonEnergyBarSetup;
        [SerializeField] private SoldierSpawnControllerSetup _playerSpawnControllerSetup;
        [SerializeField] private RaycastSettings _soldierSelectorSettings;
        [SerializeField] private RaycastSettings _controlPointSelectorSettings;
        [SerializeField] private ShootMinigameSetup _shootMinigameSetup;
        [SerializeField] private float _spawnRadius = 2f;
        [SerializeField] private float _startSpawnDelay = 2f;

        private ICoroutineRunner _coroutineRunner;
        private AssetLoader _assetLoader;
        private GenericSpawnableObjectFactory<SoldierSetup> _soldierFactory;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanger;
        private ControlPointDatabase _controlPointDatabase;
        private InputService _input;
        private TimeController _timeController;

        [Inject]
        private void Init(AssetLoader assetLoader, GenericSpawnableObjectFactory<SoldierSetup> soldierFactory, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase, InputService input, TimeController timeController)
        {
            ExceptionsTest.NullRefMethodTest(nameof(PlayerFactory), nameof(Init), assetLoader, soldierFactory, coroutineRunner,
                projectileSpawner, colorChanger, controlPointDatabase, input, timeController);

            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _soldierFactory = soldierFactory;
            _projectileSpawner = projectileSpawner;
            _colorChanger = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _input = input;
            _timeController = timeController;
        }

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(PlayerFactory), nameof(Awake), _soldierSpawnerSettings, _soldierDespawnDetector,
                _playerCannonEnergyBarSetup, _playerSpawnControllerSetup, _soldierSelectorSettings, _controlPointSelectorSettings,
                _shootMinigameSetup);
        }

        public Player CreatePlayer(Team team, CannonModel cannon, CannonData cannonData, float soldierSpawnDelay, Transform soldierSpawnPoint, SoldierData soldierStats)
        {
            ExceptionsTest.NullRefMethodTest(nameof(PlayerFactory), nameof(CreatePlayer), team, cannon, 
                cannonData, soldierSpawnPoint, soldierStats);

            CannonEnergyBarModel cannonEnergyBar = _playerCannonEnergyBarSetup.CreateCannonEnergyBar(team,
                _controlPointDatabase, cannonData.MaxEnergy, _coroutineRunner);

            SoldierSpawner spawner = new(team, soldierStats, _coroutineRunner, _colorChanger,
                _soldierSpawnerSettings, _soldierFactory);

            SoldierSpawnControllerModel spawnController = _playerSpawnControllerSetup.CreateSoldierSpawnController(
                _startSpawnDelay, soldierSpawnDelay, _spawnRadius, soldierSpawnPoint,
                _soldierDespawnDetector, team, spawner, _coroutineRunner);

            ShootMinigameModel shootMinigame = _shootMinigameSetup.CreateShootMinigameModel(cannonEnergyBar,
                _timeController, _coroutineRunner);
            Player player = new(cannon, cannonEnergyBar, shootMinigame,
                spawnController, CreateSoldierCommandController(team));

            return player;
        }

        private SoldierCommandController CreateSoldierCommandController(Team team)
        {
            ExceptionsTest.NullRefMethodTest(nameof(PlayerFactory), nameof(CreatePlayer), team);

            FloatingPointer floatingPointer = _assetLoader.Instantiate<FloatingPointer>(FloatingPointerAssetPath);
            SoldierSelector soldierSelector = new(_soldierSelectorSettings);
            ControlPointSelector controlPointSelector = new(_controlPointSelectorSettings);

            SoldierCommandController controller = new(0.1f, soldierSelector,
                controlPointSelector, floatingPointer, _coroutineRunner, team, _input);

            return controller;
        }
    }
}
