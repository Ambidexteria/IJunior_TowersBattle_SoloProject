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

namespace Base.Services.Factories.Game
{
    public class GameSceneFactory : MonoBehaviour
    {
        private const string NPCCannon = "GameLogic/Cannon/NPCCannon Variant";

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
        private IPersisentDataService _dataSerive;
        private Wallet _wallet;

        private StageInfo _stageInfo;

        private CannonModel _playerCannon;
        private CannonModel _NPCCannon;
        private BattleController _battleController;
        private Stage _stage;

        [Inject]
        private void Init(Infrastructure.Game game, AssetLoader assetLoader, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase,
            SceneChanger sceneChanger, IPersisentDataService dataService, Wallet wallet)
        {
            _game = game;
            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _projectileSpawner = projectileSpawner;
            _colorChanher = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _sceneChanger = sceneChanger;
            _dataSerive = dataService;
            _wallet = wallet;

            _stageInfo = _dataSerive.GameData.StagesData.GetSelectedStage();
        }

        private void Awake()
        {
            LoadStage();
            _controlPointDatabase.SetControlPointsOnStage(_stage.GetControlPoints());

            Player player = CreatePlayer();
            NPC npc = CreateNPC();

            _playerCannon.SetEnemy(_NPCCannon);
            _NPCCannon.SetEnemy(_playerCannon);

            _battleController = new BattleController(player, npc, _uiFactory.GetUIStateMachine(), 
                _uiFactory.GetBattleEndModel(_game, _wallet, _stageInfo, _dataSerive.GameData.Score));
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

        public NPC CreateNPC()
        {
            Team team = new Team(TeamType.NPC);

            _NPCCannon =  _npcCannonSetup.CreateCannonModel(team, _stageInfo.EnemyCannon.Damage, _colorChanher, _projectileSpawner, 
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

        private void LoadStage()
        {
            _stage = _assetLoader.Instantiate<Stage>(_stageInfo.AssetPath);
        }

        private CannonModel CreateCannon(string assetPath, Team team, int damage, HealthModel health)
        {
            CannonSetup setup = _assetLoader.Instantiate<CannonSetup>(assetPath);
            CannonModel model = setup.CreateCannonModel(team, damage, _colorChanher, _projectileSpawner, health);

            return model;
        }
    }
}
