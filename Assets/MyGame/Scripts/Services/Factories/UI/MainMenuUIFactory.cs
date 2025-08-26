using Base.GameLogic.UpgradeSystem;
using Base.Infrastructure;
using Base.PLayer;
using Base.Services.Audio;
using Base.Services.Localization;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using Base.Services.TimeManagment;
using Base.UI.Settings;
using Base.UI.StateMachine;
using Base.UI.StateMachine.States;
using UnityEngine;
using YG;
using Zenject;

namespace Base.Services.Factories.UI
{
    public class MainMenuUIFactory : MonoBehaviour
    {
        [SerializeField] private SettingsMenuSetup _settingsSetup;
        [SerializeField] private AuthorizationMenu _authorizationMenu;

        [SerializeField] private ButtonClickHandler _startBattleButton;
        [SerializeField] private ButtonClickHandler _openStagesButton;
        [SerializeField] private ButtonClickHandler _openShopButton;
        [SerializeField] private ButtonClickHandler _openSettingsButton;
        [SerializeField] private ButtonClickHandler _openLeaderboardButton;
        [SerializeField] private ButtonClickHandler _openResetProgressMenuButton;

        [SerializeField] private UIWindowController _mainButtonsWindow;
        [SerializeField] private UIWindowController _stagesWindow;
        [SerializeField] private UIWindowController _shopWindow;
        [SerializeField] private UIWindowController _settingsWindow;
        [SerializeField] private UIWindowController _leaderboardWindow;
        [SerializeField] private UIWindowController _authorizationWindow;
        [SerializeField] private UIWindowController _resetProgressWindow;

        [SerializeField] private ButtonClickHandler _closeStagesButton;
        [SerializeField] private ButtonClickHandler _closeShopButton;
        [SerializeField] private ButtonClickHandler _closeSettingsButton;
        [SerializeField] private ButtonClickHandler _closeLeaderboardButton;
        [SerializeField] private ButtonClickHandler _confirmResetProgressButton;
        [SerializeField] private ButtonClickHandler _cancelResetProgressButton;

        private ISaveLoadService _saveLoadService;
        private TimeController _timeController;
        private Wallet _wallet;
        private RegularUpgradeSystem _upgradeSystem;
        private MainMenuUIStateMachine _stateMachine;
        private IPersisentDataService _dataService;
        private IAudioVolumeControllerService _volumeControllerService;
        private Infrastructure.Game _game;
        private ILocalizationService _localizationService;
        private AudioPlayerService _auidoPlayer;
        private LoadingCurtain _loadingCurtain;

        [Inject]
        private void Init(
            TimeController timeController, 
            Wallet wallet, 
            RegularUpgradeSystem upgradeSystem,
            ISaveLoadService saveLoadService, 
            IPersisentDataService dataService,
            IAudioVolumeControllerService volumeControllerService, 
            Infrastructure.Game game,
            ILocalizationService localizationService, 
            AudioPlayerService audioPlayer, 
            LoadingCurtain loadingCurtain)
        {
            _timeController = timeController;
            _wallet = wallet;
            _upgradeSystem = upgradeSystem;
            _saveLoadService = saveLoadService;
            _dataService = dataService;
            _volumeControllerService = volumeControllerService;
            _game = game;
            _localizationService = localizationService;
            _auidoPlayer = audioPlayer;
            _loadingCurtain = loadingCurtain;
        }

        private void Awake()
        {
            _timeController.SetDefaultTimeScale();
            CreateUIStateMachine();
            CreateSettings();

            _authorizationMenu.Init(_stateMachine);
        }

        private void OnEnable()
        {
            _auidoPlayer.PlayMainMenuMusic();

            _startBattleButton.Clicked += OnStartButtonClicked;

            _openStagesButton.Clicked += OnOpenStagesButtonClicked;
            _openShopButton.Clicked += OnOpenShopButtonClicked;
            _openSettingsButton.Clicked += OnOpenSettingsButtonClicked;
            _openLeaderboardButton.Clicked += OnOpenLeaderboardButtonClicked;
            _openResetProgressMenuButton.Clicked += OnOpenResetProgressMenuButton;

            _closeStagesButton.Clicked += OnCloseWindowButtonClicked;
            _closeShopButton.Clicked += OnCloseWindowButtonClicked;
            _closeSettingsButton.Clicked += OnCloseWindowButtonClicked;
            _closeLeaderboardButton.Clicked += OnCloseWindowButtonClicked;
            _cancelResetProgressButton.Clicked += OnCloseWindowButtonClicked;
            _confirmResetProgressButton.Clicked += OnCloseWindowButtonClicked;

            _loadingCurtain.Faded += OnLoadingCurtainFaded;
        }

        private void OnDisable()
        {
            _startBattleButton.Clicked -= OnStartButtonClicked;

            _openStagesButton.Clicked -= OnOpenStagesButtonClicked;
            _openShopButton.Clicked -= OnOpenShopButtonClicked;
            _openSettingsButton.Clicked -= OnOpenSettingsButtonClicked;
            _openLeaderboardButton.Clicked -= OnOpenLeaderboardButtonClicked;
            _openResetProgressMenuButton.Clicked -= OnOpenResetProgressMenuButton;

            _closeStagesButton.Clicked -= OnCloseWindowButtonClicked;
            _closeShopButton.Clicked -= OnCloseWindowButtonClicked;
            _closeSettingsButton.Clicked -= OnCloseWindowButtonClicked;
            _closeLeaderboardButton.Clicked -= OnCloseWindowButtonClicked;
            _cancelResetProgressButton.Clicked -= OnCloseWindowButtonClicked;
            _confirmResetProgressButton.Clicked -= OnCloseWindowButtonClicked;

            _loadingCurtain.Faded -= OnLoadingCurtainFaded;
        }

        private void CreateUIStateMachine()
        {
            _stateMachine = new MainMenuUIStateMachine(
                _mainButtonsWindow, 
                _shopWindow, 
                _stagesWindow, 
                _settingsWindow,
                _leaderboardWindow, 
                _authorizationWindow, 
                _resetProgressWindow);

            _stateMachine.Enter<MainMenuState>();
        }

        private void CreateSettings()
        {
            _settingsSetup.CreateModel(
                _volumeControllerService,
                _saveLoadService, 
                _dataService.GameData.GameSettings,
                _localizationService);
        }

        private void OnLoadingCurtainFaded()
        {
            YG2.GameReadyAPI();
        }

        private void OnStartButtonClicked()
        {
            _game.LoadGameScene();
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

        private void OnOpenLeaderboardButtonClicked()
        {
            if (YG2.player.auth)
                _stateMachine.Enter<LeaderboardWindowState>();
            else
                _stateMachine.Enter<AutorizationWindowState>();
        }

        private void OnOpenResetProgressMenuButton()
        {
            _stateMachine.Enter<ResetProgressWindowState>();
        }

        private void OnCloseWindowButtonClicked()
        {
            _stateMachine.Enter<MainMenuState>();
        }  
    }
}
