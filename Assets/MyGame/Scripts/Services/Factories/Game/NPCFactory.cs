using Base.Data.Game;
using Base.GameLogic.Cannon;
using Base.Infrastructure;
using Base.PLayer;
using Base.Services.AssetManagment;
using Base.Services.Input;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using Base.Services.SceneManagment;
using Base.Services.TimeManagment;
using Base.Soldier;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Base.Services.Factories.Game
{
    public class NPCFactory : MonoBehaviour
    {
        [SerializeField] private SpawnerSettings _spawnerSettings;
        [SerializeField] private SoldierForDespawnDetector _soldierDespawnDetector;
        [SerializeField] private HealthSetup _npcHealthSetup;
        [SerializeField] private CannonEnergyBarSetup _npcCannonEnergyBarSetup;
        [SerializeField] private SoldierSpawnControllerSetup _npcSpawnControllerSetup;
        [SerializeField] private float _soldierNextCommandDelay = 5f;
        [SerializeField] private float _spawnRadius = 2f;

        private ICoroutineRunner _coroutineRunner;
        private AssetLoader _assetLoader;
        private CannonProjectileSpawner _projectileSpawner;
        private TeamColorChanger _colorChanher;
        private ControlPointDatabase _controlPointDatabase;
        private IPersisentDataService _dataSerive;
        private ISaveLoadService _saveLoadService;
        private GenericSpawnableObjectFactory<SoldierSetup> _soldierFactory;


        [Inject]
        private void Init(Infrastructure.Game game, AssetLoader assetLoader, ICoroutineRunner coroutineRunner,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            ControlPointDatabase controlPointDatabase,
            SceneChanger sceneChanger, IPersisentDataService dataService,
            ISaveLoadService saveLoadService, GenericSpawnableObjectFactory<SoldierSetup> soldierFactory)
        {
            ExceptionsTest.NullRefMethodTest(nameof(NPCFactory), nameof(Init), game, assetLoader, coroutineRunner,
                projectileSpawner, colorChanger,controlPointDatabase,sceneChanger, dataService, saveLoadService, soldierFactory);

            _coroutineRunner = coroutineRunner;
            _assetLoader = assetLoader;
            _projectileSpawner = projectileSpawner;
            _colorChanher = colorChanger;
            _controlPointDatabase = controlPointDatabase;
            _dataSerive = dataService;
            _saveLoadService = saveLoadService;
            _soldierFactory = soldierFactory;
        }

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(NPCFactory), nameof(Awake),_spawnerSettings,
                _soldierDespawnDetector, _npcHealthSetup,_npcCannonEnergyBarSetup,_npcSpawnControllerSetup);
        }

        public NPC CreateNPC(Team team, CannonModel cannon, CannonData cannonData, SoldierData soldierData, Transform soldierSpawnPoint)
        {
            ExceptionsTest.NullRefMethodTest(nameof(NPCFactory), nameof(CreateNPC), team, cannon, 
                cannonData, soldierData, soldierSpawnPoint);

            CannonEnergyBarModel energyBar = _npcCannonEnergyBarSetup.CreateCannonEnergyBar(team,
                _controlPointDatabase, cannonData.MaxEnergy, _coroutineRunner);

            SoldierSpawner spawner = new (team, soldierData, _coroutineRunner, _colorChanher, _spawnerSettings, _soldierFactory);

            SoldierSpawnControllerModel spawnController = _npcSpawnControllerSetup.CreateSoldierSpawnController(soldierData.SpawnDelay,
                _spawnRadius ,soldierSpawnPoint, _soldierDespawnDetector, team, spawner, _coroutineRunner);

            NPCCannonController cannonController = new(cannon, energyBar);

            NPCSoldierController soldierController = new(_controlPointDatabase,
                spawnController, soldierData.SpawnDelay, _soldierNextCommandDelay, team, _coroutineRunner);

            NPC npc = new(cannonController, soldierController, spawnController);

            return npc;
        }
    }
}
