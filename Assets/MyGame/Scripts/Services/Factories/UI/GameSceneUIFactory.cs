using Base.Data;
using Base.Data.Game;
using Base.GameLogic;
using Base.Health;
using Base.PLayer;
using Base.Services.Audio;
using Base.Services.Localization;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using Base.Services.TimeManagment;
using Base.UI.PauseMenu;
using Base.UI.RewardForAds;
using Base.UI.Settings;
using Base.UI.StateMachine;
using UnityEngine;
using Zenject;

namespace Base.Services.Factories.UI
{
    public class GameSceneUIFactory : MonoBehaviour
    {
        [SerializeField] private BattleEndSetup _battleEndSetup;
        [SerializeField] private PauseMenuSetup _pauseMenuSetup;
        [SerializeField] private SettingsMenuSetup _settingsMenuSetup;
        [SerializeField] private RestoreHealthForRewardAdsSetup _restoreHealthForRewardAdsSetup;

        [SerializeField] private UIWindowController _cannonsHUD;
        [SerializeField] private UIWindowController _shootMinigameUI;
        [SerializeField] private UIWindowController _pauseWindow;
        [SerializeField] private UIWindowController _settingsWindow;
        [SerializeField] private UIWindowController _battleEndWindow;
        [SerializeField] private UIWindowController _restoreHealthForRewardAds;

        [SerializeField] private ButtonClickHandler _launchShootMinigameButton;
        [SerializeField] private ButtonClickHandler _pauseButton;
        [SerializeField] private ButtonClickHandler _shootButton;
        [SerializeField] private ButtonClickHandler _openSettingsButton;
        [SerializeField] private ButtonClickHandler _resumeButton;
        [SerializeField] private ButtonClickHandler _closeSettingsWindowButton;

        private TimeController _timeController;
        private GameUIStateMachine _uiStateMachine;
        private IAudioVolumeControllerService _volumeControllerService;
        private ISaveLoadService _saveLoadService;
        private IPersisentDataService _dataService;
        private ILocalizationService _localizationService;
        private AudioPlayerService _audioPlayer;
        private Infrastructure.Game _game;

        [Inject]
        private void Init(
            TimeController timeController,
            IAudioVolumeControllerService volumeControllerService, 
            ISaveLoadService saveLoadService,
            IPersisentDataService dataService, 
            ILocalizationService localizationService,
            AudioPlayerService audioPlayer, 
            Infrastructure.Game game)
        {
            _timeController = timeController;
            _volumeControllerService = volumeControllerService;
            _saveLoadService = saveLoadService;
            _dataService = dataService;
            _localizationService = localizationService;
            _audioPlayer = audioPlayer;
            _game = game;
        }

        private void Awake()
        {
            _timeController.Resume();
            CreateUIStateMachine();
        }

        private void OnEnable()
        {
            _audioPlayer.PlayGameSceneMusic();

            _launchShootMinigameButton.Clicked += OnLaunchShootMinigameButtonClicked;
            _pauseButton.Clicked += OnPauseButtonClicked;
            _shootButton.Clicked += OnShootButtonCliked;
            _openSettingsButton.Clicked += OnOpenSettingsButtonClicked;
            _resumeButton.Clicked += OnResumeButtonClicked;
            _closeSettingsWindowButton.Clicked += OnCloseSettingsButtonClicked;
        }

        private void OnDisable()
        {
            _launchShootMinigameButton.Clicked -= OnLaunchShootMinigameButtonClicked;
            _pauseButton.Clicked -= OnPauseButtonClicked;
            _shootButton.Clicked -= OnShootButtonCliked;
            _openSettingsButton.Clicked -= OnOpenSettingsButtonClicked;
            _resumeButton.Clicked -= OnResumeButtonClicked;
            _closeSettingsWindowButton.Clicked -= OnCloseSettingsButtonClicked;
        }

        public GameUIStateMachine GetUIStateMachine()
        {
            if (_uiStateMachine == null)
                CreateUIStateMachine();

            return _uiStateMachine;
        }

        public RestoreHealthForRewardAdsModel GetRestoreHealthForRewardAdsModel(HealthModel health)
        {
            return _restoreHealthForRewardAdsSetup.Create(health);
        }

        public BattleEndModel GetBattleEndModel(
            Infrastructure.Game game, 
            Wallet wallet, 
            StageInfo stageInfo, 
            PlayerScore score, 
            StagesData stagesData)
        {
            return _battleEndSetup.Create(
                game, 
                wallet, 
                score,
                _saveLoadService, 
                stageInfo.WinReward, 
                stageInfo.DefeatReward,
                stagesData, 
                _audioPlayer);
        }

        private void CreateUIStateMachine()
        {
            _uiStateMachine = new GameUIStateMachine(
                _cannonsHUD, 
                _shootMinigameUI,
                _pauseWindow, 
                _settingsWindow,
                _battleEndWindow, 
                _restoreHealthForRewardAds);

            _pauseMenuSetup.CreatePauseMenu(_game);

            _settingsMenuSetup.CreateModel(
                _volumeControllerService, 
                _saveLoadService, 
                _dataService.GameData.GameSettings,
                _localizationService);

            _uiStateMachine.Enter<CannonsHUDState>();
        }

        private void OnLaunchShootMinigameButtonClicked()
        {
            _uiStateMachine.Enter<ShootMinigameState>();
            _timeController.SetSlowMotionTimeScale();
        }

        private void OnPauseButtonClicked()
        {
            _uiStateMachine.Enter<PauseState>();
            _timeController.Pause();
        }

        private void OnShootButtonCliked()
        {
            _uiStateMachine.Enter<CannonsHUDState>();
            _timeController.SetDefaultTimeScale();
        }

        private void OnOpenSettingsButtonClicked()
        {
            _uiStateMachine.Enter<SettingsMenuState>();
        }

        private void OnResumeButtonClicked()
        {
            _uiStateMachine.Enter<CannonsHUDState>();
            _timeController.Resume();
        }

        private void OnCloseSettingsButtonClicked()
        {
            _uiStateMachine.Enter<PauseState>();
        }
    }
}
