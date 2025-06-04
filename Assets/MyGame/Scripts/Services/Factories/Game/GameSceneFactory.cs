using UnityEngine;
using Base.Services.AssetManagment;
using Base.Services.Input;
using Base.Services.TimeManagment;
using Base.GameLogic.Cannon;
using Zenject;
using Base.Infrastructure;
using Base.Soldier;
using Base.Health;
using Base.Services.Factories.UI;
using Base.Services.SceneManagment;
using Base.Services.SaveLoad;
using Base.Services.PersistentProgress;
using Base.GameLogic;
using Base.PLayer;
using Base.Data.Player;
using Base.Data;

namespace Base.Services.Factories.Game
{
    public class GameSceneFactory : MonoBehaviour
    {
        private const string PlayerCannon = "GameLogic/Cannon/PlayerCannon Variant";
        private const string NPCCannon = "GameLogic/Cannon/NPCCannon Variant";
        private const string FloatingPointer = "GameLogic/Soldier/FloatingPointer";

        [SerializeField] private GameSceneUIFactory _uiFactory;
        [SerializeField] private SoldierForDespawnDetector _soldierDespawnDetector;

        [Header("Player")]
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private CannonSetup _playerCannonSetup;
        [SerializeField] private HealthSetup _playerCannonHealthSetup;

        [Header("NPC")]
        [SerializeField] private Transform _npcSpawnPoint;
        [SerializeField] private HealthSetup _npcHealthSetup;
        [SerializeField] private CannonEnergyBarSetup _npcCannonEnergyBarSetup;
        [SerializeField] private SoldierSpawnControllerSetup _npcSpawnControllerSetup;
        [SerializeField] private float _npcNextCommandDelay = 5f;

        private InputService _input;
        private PlayerInputController _playerInputController;

        private SceneChanger _sceneChanger;
        private TimeController _timeController;
        private Infrastructure.Game _game;
        private ICoroutineRunner _coroutineRunner;
        private SoldierSpawner _soldierSpawner;
        private AssetLoader _assetLoader;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanher;
        private ControlPointDatabase _controlPointDatabase;
        private IPersisentDataService _dataSerive;
        private Wallet _wallet;
        private ISaveLoadService _saveLoadService;

        private StageInfo _stageInfo;

        private CannonModel _playerCannon;
        private CannonModel _NPCCannon;
        private BattleController _battleController;

        [Inject]
        private void Init(Infrastructure.Game game, AssetLoader assetLoader, SoldierSpawner soldierSpawner, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase, InputService input, TimeController timeController, 
            SceneChanger sceneChanger, IPersisentDataService dataService, Wallet wallet, 
            ISaveLoadService saveLoadService)
        {
            _game = game;
            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _soldierSpawner = soldierSpawner;
            _projectileSpawner = projectileSpawner;
            _colorChanher = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _timeController = timeController;
            _input = input;
            _sceneChanger = sceneChanger;
            _dataSerive = dataService;
            _wallet = wallet;
            _saveLoadService = saveLoadService;

            _stageInfo = _dataSerive.PlayerProgress.GameSettings.SelectedStage;
        }

        private void Awake()
        {
            LoadStage();

            Player player = CreatePlayer();
            NPC npc = CreateNPC(_stageInfo.EnemyCannon, _stageInfo.EnemySoldier);

            _playerCannon.SetEnemy(_NPCCannon);
            _NPCCannon.SetEnemy(_playerCannon);

            _battleController = new BattleController(player, npc, _uiFactory.GetUIStateMachine(), 
                _uiFactory.GetBattleEndModel(_game, _wallet));
        }

        private Player CreatePlayer()
        {
            Team team = new Team(TeamType.Player);

            _playerCannon = _playerCannonSetup.CreateCannonModel(team, _dataSerive.PlayerProgress.CannonData.Damage,
                _colorChanher, _projectileSpawner, 
                _playerCannonHealthSetup.CreateHealth(_dataSerive.PlayerProgress.CannonData.MaxHealth,
                _coroutineRunner));

            return _playerFactory.CreatePlayer(team, _playerCannon, _dataSerive.PlayerProgress.CannonData, 
                _dataSerive.PlayerProgress.SoldierData.SpawnDelay);
        }

        private void OnEnable()
        {
            _battleController.Enable();

            _sceneChanger.ChangingScene += _battleController.Disable;
        }

        private void OnDisable()
        {
            _battleController.Disable();

            _sceneChanger.ChangingScene -= _battleController.Disable;
        }

        public NPC CreateNPC(CannonData cannonData, SoldierData soldierData)
        {
            Team team = new Team(TeamType.NPC);
            CannonEnergyBarModel energyBar = _npcCannonEnergyBarSetup.CreateCannonEnergyBar(team,
                _controlPointDatabase, cannonData.MaxEnergy, _coroutineRunner);

            _NPCCannon = CreateCannon(NPCCannon, team, cannonData.Damage, _npcHealthSetup.CreateHealth(cannonData.MaxHealth, _coroutineRunner));
            SoldierSpawnControllerModel spawnController = _npcSpawnControllerSetup.CreateSoldierSpawnController(soldierData.SpawnDelay, 
                _npcSpawnPoint, _soldierDespawnDetector, team, _soldierSpawner, _coroutineRunner);

            NPCCannonController cannonController = new (_NPCCannon, energyBar);

            NPCSoldierController soldierController = new (_controlPointDatabase,
                spawnController, soldierData.SpawnDelay, _npcNextCommandDelay, team, _coroutineRunner);

            NPC npc = new(cannonController, soldierController, spawnController);

            return npc;
        }

        private void LoadStage()
        {
            _assetLoader.Instantiate(_stageInfo.AssetPath);
        }

        private CannonModel CreateCannon(string assetPath, Team team, int damage, HealthModel health)
        {
            CannonSetup setup = _assetLoader.Instantiate<CannonSetup>(assetPath);
            CannonModel model = setup.CreateCannonModel(team, damage, _colorChanher, _projectileSpawner, health);

            return model;
        }
    }
}
