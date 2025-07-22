using System;
using UnityEngine;
using Base.Services.AssetManagment;
using Base.GameLogic.Cannon;
using Zenject;
using Base.Infrastructure;
using Base.Health;
using Base.Services.Factories.UI;
using Base.Services.SceneManagment;
using Base.Services.PersistentProgress;
using Base.GameLogic;
using Base.PLayer;
using Base.Data;
using Base.Services.TimeManagment;
using Base.Services.Audio;
using Base.Services.PluginYG;
using Base.GameLogic.Tutorial;
using Base.UI.StateMachine;

namespace Base.Services.Factories.Game
{
    public class GameSceneFactory : MonoBehaviour
    {
        private const string NPCCannon = "GameLogic/Cannon/NPCCannon Variant";

        [SerializeField] private bool _enableTutorial = false;
        [SerializeField] private TutorialBattleController _tutorialBattleController;
        [SerializeField] private GameSceneUIFactory _uiFactory;
        [SerializeField] private SoldierForDespawnDetector _soldierDespawnDetector;

        [Header("Player")]
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private CannonSetup _playerCannonSetup;
        [SerializeField] private HealthSetup _playerCannonHealthSetup;

        [Header("NPC")]
        [SerializeField] private NPCFactory _npcFactory;
        [SerializeField] private CannonSetup _npcCannonSetup;
        [SerializeField] private HealthSetup _npcHealthSetup;

        private SceneChanger _sceneChanger;
        private Infrastructure.Game _game;
        private ICoroutineRunner _coroutineRunner;
        private AssetLoader _assetLoader;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanher;
        private ControlPointDatabase _controlPointDatabase;
        private InputService _inputService;
        private IPersisentDataService _dataSerive;
        private Wallet _wallet;
        private TimeController _timeController;
        private AudioPlayerService _audioPlayer;
        private StageInfo _stageInfo;

        private CannonModel _playerCannon;
        private CannonModel _NPCCannon;
        private BattleController _battleController;
        private Stage _stage;

        [Inject]
        private void Init(Infrastructure.Game game, AssetLoader assetLoader, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase, InputService inputService,
            SceneChanger sceneChanger, IPersisentDataService dataService, Wallet wallet,
            TimeController timeController, AudioPlayerService audioPlayer)
        {
            ExceptionsTest.NullRefMethodTest(nameof(GameSceneFactory), nameof(Init), game, assetLoader, coroutineRunner,
             projectileSpawner, colorChanger, controlPointDatabase, sceneChanger, dataService, wallet);

            _game = game;
            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _projectileSpawner = projectileSpawner;
            _colorChanher = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _inputService = inputService;
            _sceneChanger = sceneChanger;
            _dataSerive = dataService;
            _wallet = wallet;
            _timeController = timeController;
            _audioPlayer = audioPlayer;
        }

        private void Awake()
        {

            ExceptionsTest.NullRefMethodTest(nameof(GameSceneFactory), nameof(Awake),
                _uiFactory, _soldierDespawnDetector, _playerFactory, _playerCannonSetup, _playerCannonHealthSetup,
                _npcFactory, _npcCannonSetup, _npcHealthSetup);

            _stageInfo = _dataSerive.GameData.StagesData.GetSelectedStage() ?? throw new NullReferenceException(nameof(_stageInfo));

            _enableTutorial = _dataSerive.GameData.GameSettings.TutorialEnabled;

            _stage = _assetLoader.Instantiate<Stage>(_stageInfo.AssetPath) ?? throw new NullReferenceException(nameof(_stage));

            _controlPointDatabase.Init(_stage.GetControlPoints(), _stage.PlayerSoldierSpawnPoint);

            Player player = CreatePlayer();
            NPC npc = CreateNPC();

            _playerCannon.SetEnemy(_NPCCannon);
            _NPCCannon.SetEnemy(_playerCannon);

            GameUIStateMachine gameUIStateMachine = _uiFactory.GetUIStateMachine();
            BattleEndModel battleEndModel = _uiFactory.GetBattleEndModel(_game, _wallet, _stageInfo,
                _dataSerive.GameData.Score, _dataSerive.GameData.StagesData);

            _battleController = new BattleController(player, npc, gameUIStateMachine, battleEndModel,
                _timeController, _uiFactory.GetRestoreHealthForRewardAdsModel(_playerCannonHealthSetup.GetModel()));

            _tutorialBattleController.Init(player, npc, _controlPointDatabase, _dataSerive.GameData.GameSettings);

            MetricsService.CallStageLoadedEvent(_stageInfo.Name);

            if (_enableTutorial)
                _tutorialBattleController.Enable();

            _battleController.Enable();
        }

        private void OnEnable()
        {
            _sceneChanger.ChangingScene += _battleController.Disable;
        }

        private void OnDisable()
        {
            if (_enableTutorial)
                _tutorialBattleController.Disable();
            else
                _battleController.Disable();

            _sceneChanger.ChangingScene -= _battleController.Disable;
        }

        private void LoadTutorial()
        {
            //_stage = _assetLoader.Instantiate<Stage>(TutorialStageAssetPath) ?? throw new NullReferenceException(nameof(_stage));
        }

        private NPC CreateNPC()
        {
            Team team = new Team(TeamType.NPC);

            _NPCCannon = _npcCannonSetup.CreateCannonModel(team, _stageInfo.EnemyCannon.Damage, _colorChanher, _projectileSpawner,
                _npcHealthSetup.CreateHealth(_stageInfo.EnemyCannon.MaxHealth, _coroutineRunner));

            return _npcFactory.CreateNPC(team, _NPCCannon, _stageInfo.EnemyCannon, _stageInfo.EnemySoldier, _stage.NPCSoldierSpawnPoint);
        }

        private Player CreatePlayer()
        {
            Team team = new Team(TeamType.Player);

            _playerCannon = _playerCannonSetup.CreateCannonModel(team, _dataSerive.GameData.CannonData.Damage,
                _colorChanher, _projectileSpawner,
                _playerCannonHealthSetup.CreateHealth(_dataSerive.GameData.CannonData.MaxHealth,
                _coroutineRunner));

            return _playerFactory.CreatePlayer(team, _playerCannon, _dataSerive.GameData.CannonData,
                _dataSerive.GameData.SoldierData.SpawnDelay, _stage.PlayerSoldierSpawnPoint, _dataSerive.GameData.SoldierData);
        }

        private CannonModel CreateCannon(string assetPath, Team team, int damage, HealthModel health)
        {
            ExceptionsTest.NullRefMethodTest(nameof(GameSceneFactory), nameof(CreateCannon), team, health);

            CannonSetup setup = _assetLoader.Instantiate<CannonSetup>(assetPath);
            CannonModel model = setup.CreateCannonModel(team, damage, _colorChanher, _projectileSpawner, health);

            return model;
        }
    }
}
