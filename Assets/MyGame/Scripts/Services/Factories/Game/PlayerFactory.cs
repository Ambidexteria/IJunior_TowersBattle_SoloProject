using Base.Data.Game;
using Base.GameLogic.Cannon;
using Base.GameLogic.ShootMinigame;
using Base.Infrastructure;
using Base.Services.AssetManagment;
using Base.Services.Audio;
using Base.Services.TimeManagment;
using Base.Soldier;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] private Image _selectionBox;

        private ICoroutineRunner _coroutineRunner;
        private AssetLoader _assetLoader;
        private GenericSpawnableObjectFactory<SoldierSetup> _soldierFactory;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanger;
        private ControlPointDatabase _controlPointDatabase;
        private InputService _input;
        private TimeController _timeController;
        private AudioPlayerService _audioPlayer;

        [Inject]
        private void Init(AssetLoader assetLoader, GenericSpawnableObjectFactory<SoldierSetup> soldierFactory, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase, InputService input, TimeController timeController,
            AudioPlayerService audioPlayer)
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
            _audioPlayer = audioPlayer;
        }

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(PlayerFactory), nameof(Awake), _soldierSpawnerSettings, _soldierDespawnDetector,
                _playerCannonEnergyBarSetup, _playerSpawnControllerSetup, _soldierSelectorSettings, _controlPointSelectorSettings,
                _shootMinigameSetup);
        }

        public Player CreatePlayer(Team team, CannonModel cannon, CannonData cannonData, Transform soldierSpawnPoint,
            SoldierData soldierStats)
        {
            ExceptionsTest.NullRefMethodTest(nameof(PlayerFactory), nameof(CreatePlayer), team, cannon,
                cannonData, soldierSpawnPoint, soldierStats);

            CannonEnergyBarModel cannonEnergyBar = _playerCannonEnergyBarSetup.CreateCannonEnergyBar(team,
                _controlPointDatabase, cannonData.MaxEnergy, _coroutineRunner);

            SoldierSpawner spawner = new(team, soldierStats, _coroutineRunner, _colorChanger,
                _soldierSpawnerSettings, _soldierFactory, _audioPlayer);

            SoldierSpawnControllerModel spawnController = _playerSpawnControllerSetup.CreateModel(
                _startSpawnDelay, soldierStats.SpawnDelay, _spawnRadius, soldierSpawnPoint,
                _soldierDespawnDetector, team, spawner, _coroutineRunner);

            ShootMinigameModel shootMinigame = _shootMinigameSetup.CreateShootMinigameModel(cannonEnergyBar,
                _timeController, _coroutineRunner);

            SoldierSelector soldierSelector = new(_soldierSelectorSettings, _coroutineRunner, _input, _selectionBox, team, _audioPlayer);

            Player player = new(team, cannon, cannonEnergyBar, shootMinigame,
                spawnController, CreateSoldierCommandController(team), soldierSelector);

            return player;
        }

        private SoldierCommandController CreateSoldierCommandController(Team team)
        {
            ExceptionsTest.NullRefMethodTest(nameof(PlayerFactory), nameof(CreatePlayer), team);

            FloatingPointer floatingPointer = _assetLoader.Instantiate<FloatingPointer>(FloatingPointerAssetPath);
            SoldierSelector soldierSelector = new(_soldierSelectorSettings, _coroutineRunner, _input, _selectionBox, team, _audioPlayer);
            ControlPointSelector controlPointSelector = new(_controlPointSelectorSettings);

            SoldierCommandController controller = new(0.1f, soldierSelector,
                controlPointSelector, floatingPointer, _coroutineRunner, team, _input, _audioPlayer);

            return controller;
        }
    }
}
