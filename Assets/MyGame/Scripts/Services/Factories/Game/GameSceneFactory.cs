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
using Base.Health;
using Base.Services.Factories.UI;
using Base.Services.SceneManagment;

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
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private HealthSetup _playerHealthSetup;
        [SerializeField] private CannonEnergyBarSetup _playerCannonEnergyBarSetup;
        [SerializeField] private SoldierSpawnControllerSetup _playerSpawnControllerSetup;
        [SerializeField] private float _playerMaxHealth = 100f;
        [SerializeField] private float _playerMaxEnergy = 20f;
        [SerializeField] private int _playerCannonDamage = 15;
        [SerializeField] private float _playerSpawnDelay;
        [SerializeField] private RaycastSettings _soldierSelectorSettings;
        [SerializeField] private RaycastSettings _controlPointSelectorSettings;
        [SerializeField] private ShootMinigameSetup _shootMinigameSetup;

        [Header("NPC")]
        [SerializeField] private Transform _npcSpawnPoint;
        [SerializeField] private HealthSetup _npcHealthSetup;
        [SerializeField] private CannonEnergyBarSetup _npcCannonEnergyBarSetup;
        [SerializeField] private SoldierSpawnControllerSetup _npcSpawnControllerSetup;
        [SerializeField] private float _npcMaxHealth = 50f;
        [SerializeField] private float _npcMaxEnergy = 30f;
        [SerializeField] private int _npcCannonDamage = 10;
        [SerializeField] private float _npcSpawnDelay = 3f;
        [SerializeField] private float _npcNextCommandDelay = 5f;

        private InputService _input;
        private PlayerInputController _playerInputController;

        private SceneChanger _sceneChanger;
        private TimeController _timeController;
        private ICoroutineRunner _coroutineRunner;
        private SoldierSpawner _soldierSpawner;
        private AssetLoader _assetLoader;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanher;
        private ControlPointDatabase _controlPointDatabase;

        private CannonModel _playerCannon;
        private CannonModel _NPCCannon;
        private BattleController _battleController;

        [Inject]
        private void Init(AssetLoader assetLoader, SoldierSpawner soldierSpawner, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase, InputService input, TimeController timeController, 
            SceneChanger sceneChanger)
        {
            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _soldierSpawner = soldierSpawner;
            _projectileSpawner = projectileSpawner;
            _colorChanher = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _timeController = timeController;
            _input = input;
            _sceneChanger = sceneChanger;
        }

        private void Awake()
        {
            Player player = CreatePlayer();
            NPC npc = CreateNPC();

            _playerCannon.SetEnemy(_NPCCannon);
            _NPCCannon.SetEnemy(_playerCannon);

            _battleController = new BattleController(player, npc, _uiFactory.GetUIStateMachine());
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

        public Player CreatePlayer()
        {
            Team team = new Team(TeamType.Player);

            CannonEnergyBarModel cannonEnergyBar = _playerCannonEnergyBarSetup.CreateCannonEnergyBar(team, 
                _controlPointDatabase, _playerMaxEnergy, _coroutineRunner);

            _playerCannon = CreateCannon(PlayerCannon, team, _playerCannonDamage, _playerHealthSetup.CreateHealth(_playerMaxHealth, _coroutineRunner));

            SoldierSpawnControllerModel spawnController = _playerSpawnControllerSetup.CreateSoldierSpawnController(_playerSpawnDelay, _playerSpawnPoint, 
                _soldierDespawnDetector, team, _soldierSpawner, _coroutineRunner);

            ShootMinigameModel shootMinigame = _shootMinigameSetup.CreateShootMinigameModel(cannonEnergyBar,
                _timeController, _coroutineRunner, _uiFactory.GetUIStateMachine());
            Player player = new Player(_playerCannon, cannonEnergyBar, shootMinigame,
                spawnController, CreateSoldierCommandController(team));

            return player;
        }

        public NPC CreateNPC()
        {
            Team team = new Team(TeamType.NPC);
            CannonEnergyBarModel energyBar = _npcCannonEnergyBarSetup.CreateCannonEnergyBar(team,
                _controlPointDatabase, _npcMaxEnergy, _coroutineRunner);

            _NPCCannon = CreateCannon(NPCCannon, team, _npcCannonDamage, _npcHealthSetup.CreateHealth(_npcMaxHealth, _coroutineRunner));
            SoldierSpawnControllerModel spawnController = _npcSpawnControllerSetup.CreateSoldierSpawnController(_npcSpawnDelay, 
                _npcSpawnPoint, _soldierDespawnDetector, team, _soldierSpawner, _coroutineRunner);

            NPCCannonController cannonController = new NPCCannonController(_NPCCannon, energyBar);

            NPCSoldierController soldierController = new NPCSoldierController(_controlPointDatabase,
                spawnController, _npcSpawnDelay, _npcNextCommandDelay, team, _coroutineRunner);

            NPC npc = new(cannonController, soldierController, spawnController);

            return npc;
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

        private CannonModel CreateCannon(string assetPath, Team team, int damage, HealthModel health)
        {
            CannonSetup setup = _assetLoader.Instantiate<CannonSetup>(assetPath);
            CannonModel model = setup.CreateCannonModel(team, damage, _colorChanher, _projectileSpawner, health);

            return model;
        }

        private Type GetViewComponent<Type>(UIWindowController ui) where Type : MonoBehaviour
        {
            Type component = ui.GetComponentInChildren<Type>();

            return component ?? throw new NullReferenceException(component.gameObject.name);
        }
    }
}
