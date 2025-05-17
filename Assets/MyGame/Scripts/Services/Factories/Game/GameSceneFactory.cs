using UnityEngine;
using Base.Services.AssetManagment;
using Base.Services.Input;
using Base.GameLogic.Cannon;
using Zenject;
using Base.Infrastructure;
using System;
using Base.Soldier;

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
        [SerializeField] private float _playerMaxEnergy = 20f;
        [SerializeField] private float _playerSpawnDelay;
        [SerializeField] private float _npcMaxEnergy = 30;
        [SerializeField] private float _npcSpawnDelay = 3f;
        [SerializeField] private float _npcNextCommandDelay = 5f;
        [SerializeField] private RaycastSettings _soldierSelectorSettings;
        [SerializeField] private RaycastSettings _controlPointSelectorSettings;
        [SerializeField] private Canvas _playerUI;
        [SerializeField] private Canvas _npcUI;

        private PlayerHUDModel _playerHudModel;
        private InputService _input;
        private PlayerInputController _playerInputController;

        private ICoroutineRunner _coroutineRunner;
        private SoldierSpawner _soldierSpawner;
        private AssetLoader _assetLoader;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanher;
        private ControlPointDatabase _controlPointDatabase;

        private CannonModel _playerCannon;
        private CannonModel _NPCCannon;

        [Inject]
        private void Init(AssetLoader assetLoader, SoldierSpawner soldierSpawner, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase, InputService input)
        {
            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _soldierSpawner = soldierSpawner;
            _projectileSpawner = projectileSpawner;
            _colorChanher = colorChanger;
            _controlPointDatabase = controlPointDatabase;

            _input = input;
            _playerInputController = new(_input);
            _playerInputController.Enable();
        }

        public GameSceneFactory(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        private void Awake()
        {
            CreatePlayer();
            CreateNPC();

            _playerCannon.SetEnemy(_NPCCannon);
            _NPCCannon.SetEnemy(_playerCannon);
        }

        public void CreatePlayer()
        {
            Team team = new Team(TeamType.Player);

            CannonEnergyBar cannonEnergyBar = new CannonEnergyBar(team, _controlPointDatabase, _playerMaxEnergy,
                _coroutineRunner);

            _playerCannon = CreateCannon(PlayerCannon, team, 20, 2, cannonEnergyBar, _playerUI);
            cannonEnergyBar.Enable();

            CreateSoldierCommandController(team);
            SoldierSpawnControllerModel spawnController = CreateSoldierSpawnController(team, _playerSpawnDelay, 
                _playerSpawnPoint, _playerUI);

            Player player = new Player(_playerCannon, cannonEnergyBar, FindObjectOfType<ShootMinigame>(),
                spawnController);
            //player.Enable();
        }

        public void CreateNPC()
        {
            Team team = new Team(TeamType.NPC);
            CannonEnergyBar energyBar = new CannonEnergyBar(team, _controlPointDatabase, _npcMaxEnergy,
                _coroutineRunner);

            _NPCCannon = CreateCannon(NPCCannon, team, 10, 2, energyBar, _npcUI);
            SoldierSpawnControllerModel spawnController = CreateSoldierSpawnController(team, _npcSpawnDelay, 
                _npcSpawnPoint, _npcUI);


            NPCCannonController cannonController = new NPCCannonController(_NPCCannon, energyBar);

            NPCSoldierController soldierController = new NPCSoldierController(_controlPointDatabase,
                spawnController, _npcSpawnDelay, _npcNextCommandDelay, team, _coroutineRunner);

            NPC npc = new(cannonController, soldierController, spawnController);
            npc.Enable();
        }

        private SoldierSpawnControllerModel CreateSoldierSpawnController(Team team, float spawnDelay, Transform spawnPoint, Canvas uiWithView)
        {
            SoldierSpawnControllerView view = GetViewComponent<SoldierSpawnControllerView>(uiWithView);
            var model = new SoldierSpawnControllerModel(spawnDelay, spawnPoint,
                _soldierDespawnDetector, team, _soldierSpawner, _coroutineRunner);

            var presenter = new SoldierSpawnControllerPresenter(model, view);
            presenter.Enable();

            return model;
        }

        private void CreateSoldierCommandController(Team team)
        {
            FloatingPointer floatingPointer = _assetLoader.Instantiate<FloatingPointer>(FloatingPointer);
            SoldierSelector soldierSelector = new(_soldierSelectorSettings);
            ControlPointSelector controlPointSelector = new ControlPointSelector(_controlPointSelectorSettings);

            SoldierCommandController controller = new SoldierCommandController(0.1f, soldierSelector,
                controlPointSelector, floatingPointer, _coroutineRunner, team, _input);
        }

        private CannonModel CreateCannon(string assetPath, Team team, int damage, float fireDelay,
            CannonEnergyBar energyBar, Canvas ui)
        {
            CannonSetup setup = _assetLoader.Instantiate<CannonSetup>(assetPath);
            setup.Init(team, damage, fireDelay, _colorChanher, _projectileSpawner, energyBar,
                ui.GetComponentInChildren<CannonEnergyBarView>(),
                ui.GetComponentInChildren<CannonSliderHealthView>());

            return setup.GetModel();
        }

        private Type GetViewComponent<Type>(Canvas ui) where Type : MonoBehaviour
        {
            Type component = ui.GetComponentInChildren<Type>();

            return component ?? throw new NullReferenceException(nameof(GetViewComponent));
        }
    }
}
