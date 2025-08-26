using System;
using Base.Data;
using Base.Data.Game;
using Base.GameLogic;
using Base.GameLogic.Cannon;
using Base.GameLogic.Tutorial;
using Base.Infrastructure;
using Base.PLayer;
using Base.Services.AssetManagment;
using Base.Services.Audio;
using Base.Services.Factories.UI;
using Base.Services.PersistentProgress;
using Base.Services.PluginYG;
using Base.Services.SaveLoad;
using Base.Services.TimeManagment;
using Base.UI.StateMachine;
using UnityEngine;
using Zenject;

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

        private Infrastructure.Game _game;
        private ICoroutineRunner _coroutineRunner;
        private AssetLoader _assetLoader;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanher;
        private ControlPointDatabase _controlPointDatabase;
        private IPersisentDataService _dataSerive;
        private Wallet _wallet;
        private TimeController _timeController;
        private AudioPlayerService _audioPlayer;
        private ISaveLoadService _saveLoadService;
        private StageInfo _stageInfo;

        private CannonModel _playerCannon;
        private CannonModel _NPCCannon;
        private BattleController _battleController;
        private Stage _stage;

        [Inject]
        private void Init(
            Infrastructure.Game game,
            AssetLoader assetLoader,
            ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner,
            TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase,
            IPersisentDataService dataService,
            Wallet wallet,
            TimeController timeController,
            AudioPlayerService audioPlayer,
            ISaveLoadService saveLoadService)
        {
            _game = game;
            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _projectileSpawner = projectileSpawner;
            _colorChanher = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _dataSerive = dataService;
            _wallet = wallet;
            _timeController = timeController;
            _audioPlayer = audioPlayer;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _enableTutorial = _dataSerive.GameData.GameSettings.TutorialEnabled;
            _stageInfo = _dataSerive.GameData.StagesData.GetSelectedStage() ?? throw new NullReferenceException(nameof(_stageInfo));
            _stage = _assetLoader.Instantiate<Stage>(_stageInfo.AssetPath) ?? throw new NullReferenceException(nameof(_stage));
            _controlPointDatabase.Init(_stage.GetControlPoints(), _stage.PlayerSoldierSpawnPoint);

            Player player = CreatePlayer();
            NPC npc = CreateNPC();

            _playerCannon.SetEnemy(_NPCCannon);
            _NPCCannon.SetEnemy(_playerCannon);

            GameUIStateMachine gameUIStateMachine = _uiFactory.GetUIStateMachine();
            BattleEndModel battleEndModel = _uiFactory.GetBattleEndModel(
                _game,
                _wallet,
                _stageInfo,
                _dataSerive.GameData.PlayerData.Score,
                _dataSerive.GameData.StagesData);

            _battleController = new BattleController(
                player,
                npc,
                gameUIStateMachine,
                battleEndModel,
                _timeController,
                _uiFactory.GetRestoreHealthForRewardAdsModel(_playerCannonHealthSetup.GetModel()));

            _tutorialBattleController.Init(player, _controlPointDatabase, _dataSerive.GameData.GameSettings);

            MetricsService.CallStageLoadedEvent(_stageInfo.Name);

            if (_enableTutorial)
                _tutorialBattleController.Enable();

            _battleController.Enable();
        }

        private void OnDisable()
        {
            _tutorialBattleController.Disable();
            _battleController.Disable();
        }

        private NPC CreateNPC()
        {
            Team team = new Team(TeamType.NPC);

            _NPCCannon = _npcCannonSetup.CreateCannonModel(
                team, 
                _stageInfo.EnemyCannon.Damage, 
                _colorChanher,
                _projectileSpawner,
                _npcHealthSetup.CreateHealth(_stageInfo.EnemyCannon.MaxHealth, _coroutineRunner));

            return _npcFactory.CreateNPC(team, _NPCCannon, _stageInfo.EnemyCannon, _stageInfo.EnemySoldier, _stage.NPCSoldierSpawnPoint);
        }

        private Player CreatePlayer()
        {
            Team team = new Team(TeamType.Player);
            CannonData cannonData = _dataSerive.GameData.PlayerData.CannonData;

            _playerCannon = _playerCannonSetup.CreateCannonModel(
                team, 
                cannonData.Damage, 
                _colorChanher, 
                _projectileSpawner,
                _playerCannonHealthSetup.CreateHealth(cannonData.MaxHealth, _coroutineRunner));

            return _playerFactory.CreatePlayer(
                team, 
                _playerCannon, 
                cannonData,
                _stage.PlayerSoldierSpawnPoint, 
                _dataSerive.GameData.PlayerData.SoldierData);
        }
    }
}
