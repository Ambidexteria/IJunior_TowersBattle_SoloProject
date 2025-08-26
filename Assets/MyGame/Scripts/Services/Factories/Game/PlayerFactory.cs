using Base.Data.Game;
using Base.GameLogic.Cannon;
using Base.GameLogic.ShootMinigame;
using Base.Infrastructure;
using Base.Services.Audio;
using Base.Services.TimeManagment;
using Base.Soldier;
using UnityEngine;
using Zenject;

namespace Base.Services.Factories.Game
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private CannonEnergyBarSetup _playerCannonEnergyBarSetup;
        [SerializeField] private CannonEnergyRateSetup _cannonEnergyRateSetup;
        [SerializeField] private SoldierSpawnControllerSetup _playerSpawnControllerSetup;
        [SerializeField] private ShootMinigameSetup _shootMinigameSetup;

        [SerializeField] private SpawnerSettings _soldierSpawnerSettings;
        [SerializeField] private SoldierForDespawnDetector _soldierDespawnDetector;

        [SerializeField] private RaycastSettings _soldierSelectorSettings;
        [SerializeField] private RaycastSettings _controlPointSelectorSettings;
        [SerializeField] private float _spawnRadius = 2f;
        [SerializeField] private float _startSpawnDelay = 2f;
        [SerializeField] private SelectionBoxDrawer _selectionBoxDrawer;

        private ICoroutineRunner _coroutineRunner;
        private GenericSpawnableObjectFactory<SoldierSetup> _soldierFactory;
        private TeamColorChanger _colorChanger;
        private ControlPointDatabase _controlPointDatabase;
        private InputService _input;
        private TimeController _timeController;
        private AudioPlayerService _audioPlayer;

        [Inject]
        private void Init(
            GenericSpawnableObjectFactory<SoldierSetup> soldierFactory,
            ICoroutineRunner coroutineRunner,
            TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase,
            InputService input,
            TimeController timeController,
            AudioPlayerService audioPlayer)
        {
            _coroutineRunner = coroutineRunner;
            _soldierFactory = soldierFactory;
            _colorChanger = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _input = input;
            _timeController = timeController;
            _audioPlayer = audioPlayer;

            _selectionBoxDrawer.Init(_coroutineRunner);
        }

        public Player CreatePlayer(
            Team team,
            CannonModel cannon,
            CannonData cannonData,
            Transform soldierSpawnPoint,
            SoldierData soldierStats)
        {
            CannonEnergyBarModel cannonEnergyBar = _playerCannonEnergyBarSetup.CreateCannonEnergyBar(
                team,
                _controlPointDatabase,
                cannonData.MaxEnergy,
                _coroutineRunner);

            _cannonEnergyRateSetup.Create(cannonEnergyBar);

            SoldierSpawner spawner = new SoldierSpawner(
                team,
                soldierStats,
                _coroutineRunner,
                _colorChanger,
                _soldierSpawnerSettings,
                _soldierFactory,
                _audioPlayer);

            SoldierSpawnControllerModel spawnController = _playerSpawnControllerSetup.CreateModel(
                _startSpawnDelay,
                soldierStats.SpawnDelay,
                _spawnRadius,
                soldierSpawnPoint,
                _soldierDespawnDetector,
                spawner,
                _coroutineRunner);

            ShootMinigameModel shootMinigame = _shootMinigameSetup.CreateShootMinigameModel(
                cannonEnergyBar,
                _timeController,
                _coroutineRunner);

            SoldierSelector soldierSelector = new SoldierSelector(
                _soldierSelectorSettings,
                _coroutineRunner,
                _input,
                _selectionBoxDrawer,
                team,
                _audioPlayer);

            Player player = new Player(
                team,
                cannon,
                cannonEnergyBar,
                shootMinigame,
                spawnController,
                soldierSelector);

            return player;
        }
    }
}
