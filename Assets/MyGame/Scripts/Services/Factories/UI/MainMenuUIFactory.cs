using Base.GameLogic.UpgradeSystem;
using Base.PLayer;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using Base.Services.TimeManagment;
using Base.Shop;
using Base.UI.StateMachine;
using UnityEngine;
using Zenject;

namespace Base.Services.Factories.UI
{
    public class MainMenuUIFactory : MonoBehaviour
    {
        [SerializeField] private ShopSetup _shopSetup; 

        [SerializeField] private ButtonClickHandler _startBattleButton;

        [SerializeField] private ButtonClickHandler _openStagesButton;
        [SerializeField] private ButtonClickHandler _openShopButton;
        [SerializeField] private ButtonClickHandler _openSettingsButton;

        [SerializeField] private UIWindowController _mainButtonsWindow;
        [SerializeField] private UIWindowController _stagesWindow;
        [SerializeField] private UIWindowController _shopWindow;
        [SerializeField] private UIWindowController _settingsWindow;

        [SerializeField] private ButtonClickHandler _closeStagesButton;
        [SerializeField] private ButtonClickHandler _closeShopButton;
        [SerializeField] private ButtonClickHandler _closeSettingsButton;

        private ISaveLoadService _saveLoadService;
        private TimeController _timeController;
        private Wallet _wallet;
        private RegularUpgradeSystem _upgradeSystem;
        private MainMenuUIStateMachine _stateMachine;
        private UpgradePrices _upgradePrices;

        [Inject]
        private void Init(TimeController timeController, Wallet wallet, RegularUpgradeSystem upgradeSystem,
            ISaveLoadService saveLoadService, IPersisentDataService dataService)
        {
            _timeController = timeController;
            _wallet = wallet;
            _upgradeSystem = upgradeSystem;
            _saveLoadService = saveLoadService;
            _upgradePrices = dataService.PlayerProgress.UpgradePrices;
        }

        private void Awake()
        {
            _timeController.SetDefaultTimeScale();
            CreateUIStateMachine();
            CreateShop();
        }

        private void OnEnable()
        {
            _openStagesButton.Clicked += OnOpenStagesButtonClicked;
            _openShopButton.Clicked += OnOpenShopButtonClicked;
            _openSettingsButton.Clicked += OnOpenSettingsButtonClicked;

            _closeStagesButton.Clicked += OnCloseWindowButtonClicked;
            _closeShopButton.Clicked += OnCloseWindowButtonClicked;
            _closeSettingsButton.Clicked += OnCloseWindowButtonClicked;
        }

        private void OnDisable()
        {
            _openStagesButton.Clicked += OnOpenStagesButtonClicked;
            _openShopButton.Clicked += OnOpenShopButtonClicked;
            _openSettingsButton.Clicked += OnOpenSettingsButtonClicked;

            _closeStagesButton.Clicked += OnCloseWindowButtonClicked;
            _closeShopButton.Clicked += OnCloseWindowButtonClicked;
            _closeSettingsButton.Clicked += OnCloseWindowButtonClicked;
        }

        private void CreateUIStateMachine()
        {
            _stateMachine = new MainMenuUIStateMachine(_mainButtonsWindow, _shopWindow, _stagesWindow, _settingsWindow);
            _stateMachine.Enter<MainMenuState>();
        }

        private void CreateShop()
        {
            _shopSetup.Create(_wallet, _upgradeSystem, _saveLoadService, _upgradePrices);
        }

        private void OnOpenStagesButtonClicked()
        {
            _stateMachine.Enter<StagesWindowState>();
        }

        private void OnOpenShopButtonClicked()
        {
            _stateMachine.Enter<ShopWindowState>();
        }

        private void OnOpenSettingsButtonClicked()
        {
            _stateMachine.Enter<SettingsMenuState>();
        }

        private void OnCloseWindowButtonClicked()
        {
            _stateMachine.Enter<MainMenuState>();
        }
    }
}
