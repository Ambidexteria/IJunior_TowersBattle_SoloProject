using UnityEngine;
using Base.Services.AssetManagment;
using Base.Services.Input;
using Base.Services.TimeManagment;
using Base.GameLogic.Cannon;
using Zenject;
using Base.Infrastructure;
using System;
using Base.Soldier;
using Base.GameLogic.ShootMinigame;
using Base.UI.Game.StateMachine;
using Base.Health;
using UnityEditor;

namespace Base.Services.Factories.Game
{
    [Serializable]
    public class RaycastSettings
    {
        public LayerMask LayerMask;
        public float RaycastLength;
    }

    public class GameSceneFactory : MonoBehaviour
    {
        private const string PlayerCannon = "GameLogic/Cannon/PlayerCannon Variant";
        private const string NPCCannon = "GameLogic/Cannon/NPCCannon Variant";
        private const string FloatingPointer = "GameLogic/Soldier/FloatingPointer";

        [SerializeField] private SoldierForDespawnDetector _soldierDespawnDetector;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _npcSpawnPoint;
        [SerializeField] private float _playerMaxHealth = 100f;
        [SerializeField] private float _playerMaxEnergy = 20f;
        [SerializeField] private int _playerCannonDamage = 15;
        [SerializeField] private float _playerSpawnDelay;
        [SerializeField] private float _npcMaxHealth = 50f;
        [SerializeField] private float _npcMaxEnergy = 30f;
        [SerializeField] private int _npcCannonDamage = 10;
        [SerializeField] private float _npcSpawnDelay = 3f;
        [SerializeField] private float _npcNextCommandDelay = 5f;
        [SerializeField] private RaycastSettings _soldierSelectorSettings;
        [SerializeField] private RaycastSettings _controlPointSelectorSettings;
        [SerializeField] private UIWindowController _cannonsHUD;
        [SerializeField] private UIWindowController _playerCannonHUD;
        [SerializeField] private UIWindowController _npcCannonHUD;
        [SerializeField] private UIWindowController _shootMinigameUI;
        [SerializeField] private UIWindowController _pauseWindowUI;
        [SerializeField] private UIWindowController _winMessage;
        [SerializeField] private UIWindowController _defeatMessage;
        [SerializeField] private ShootMinigameSetup _shootMinigameSetup;

        private PlayerHUDModel _playerHudModel;
        private InputService _input;
        private PlayerInputController _playerInputController;

        private TimeController _timeController;
        private ICoroutineRunner _coroutineRunner;
        private SoldierSpawner _soldierSpawner;
        private AssetLoader _assetLoader;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanher;
        private ControlPointDatabase _controlPointDatabase;

        private GameUIStateMachine _uiStateMachine;
        private CannonModel _playerCannon;
        private CannonModel _NPCCannon;

        [Inject]
        private void Init(AssetLoader assetLoader, SoldierSpawner soldierSpawner, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase, InputService input, TimeController timeController)
        {
            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _soldierSpawner = soldierSpawner;
            _projectileSpawner = projectileSpawner;
            _colorChanher = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _timeController = timeController;

            _input = input;
        }

        public GameSceneFactory(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        private void Awake()
        {
            CreateUIStateMcahine();
            Player player = CreatePlayer();
            NPC npc = CreateNPC();

            _playerCannon.SetEnemy(_NPCCannon);
            _NPCCannon.SetEnemy(_playerCannon);

            BattleController battleController = new BattleController(player, npc, _uiStateMachine);
            battleController.Enable();
        }

        public Player CreatePlayer()
        {
            Team team = new Team(TeamType.Player);

            CannonEnergyBar cannonEnergyBar = new CannonEnergyBar(team, _controlPointDatabase, _playerMaxEnergy,
                _coroutineRunner);

            _playerCannon = CreateCannon(PlayerCannon, team, _playerCannonDamage, _playerMaxHealth, cannonEnergyBar,
                _playerCannonHUD);
            cannonEnergyBar.Enable();

            SoldierSpawnControllerModel spawnController = CreateSoldierSpawnController(team, _playerSpawnDelay,
                _playerSpawnPoint, _playerCannonHUD);

            ShootMinigameModel shootMinigame = _shootMinigameSetup.CreateShootMinigameModel(cannonEnergyBar,
                _timeController, _coroutineRunner, _uiStateMachine);
            Player player = new Player(_playerCannon, cannonEnergyBar, shootMinigame,
                spawnController, CreateSoldierCommandController(team));

            return player;
        }

        public NPC CreateNPC()
        {
            Team team = new Team(TeamType.NPC);
            CannonEnergyBar energyBar = new CannonEnergyBar(team, _controlPointDatabase, _npcMaxEnergy,
                _coroutineRunner);

            _NPCCannon = CreateCannon(NPCCannon, team, _npcCannonDamage, _npcMaxHealth, energyBar, _npcCannonHUD);
            SoldierSpawnControllerModel spawnController = CreateSoldierSpawnController(team, _npcSpawnDelay,
                _npcSpawnPoint, _npcCannonHUD);


            NPCCannonController cannonController = new NPCCannonController(_NPCCannon, energyBar);

            NPCSoldierController soldierController = new NPCSoldierController(_controlPointDatabase,
                spawnController, _npcSpawnDelay, _npcNextCommandDelay, team, _coroutineRunner);

            NPC npc = new(cannonController, soldierController, spawnController);

            return npc;
        }

        private void CreateUIStateMcahine()
        {
            _uiStateMachine = new GameUIStateMachine(_cannonsHUD, _shootMinigameUI,
                _pauseWindowUI, _winMessage, _defeatMessage);

            _uiStateMachine.Enter<CannonsHUDState>();
        }

        private SoldierSpawnControllerModel CreateSoldierSpawnController(Team team, float spawnDelay, Transform spawnPoint,
            UIWindowController uiWithView)
        {
            SoldierSpawnControllerView view = GetViewComponent<SoldierSpawnControllerView>(uiWithView);
            var model = new SoldierSpawnControllerModel(spawnDelay, spawnPoint,
                _soldierDespawnDetector, team, _soldierSpawner, _coroutineRunner);

            var presenter = new SoldierSpawnControllerPresenter(model, view);
            presenter.Enable();

            return model;
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

        private CannonModel CreateCannon(string assetPath, Team team, int damage, float maxHealth,
            CannonEnergyBar energyBar, UIWindowController ui)
        {
            HealthModel health = new HealthModel(maxHealth, _coroutineRunner);
            HealthPresenter presenter = new HealthPresenter(health, GetViewComponent<HealthView>(ui));
            presenter.Enable();

            CannonSetup setup = _assetLoader.Instantiate<CannonSetup>(assetPath);
            CannonModel model = setup.CreateCannonModel(team, damage, _colorChanher, _projectileSpawner, energyBar,
                GetViewComponent<CannonEnergyBarView>(ui), health);

            return model;
        }

        private Type GetViewComponent<Type>(UIWindowController ui) where Type : MonoBehaviour
        {
            Type component = ui.GetComponentInChildren<Type>();

            return component ?? throw new NullReferenceException(component.gameObject.name);
        }
    }
}
