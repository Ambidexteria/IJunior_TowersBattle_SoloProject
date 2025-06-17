using Base.Infrastructure;
using Base.Services.Audio;
using Base.Services.SceneManagment;
using Base.Services.TimeManagment;
using Base.UI.StateMachine;
using Base.UI.PauseMenu;
using Base.UI.Settings;
using UnityEngine;
using Zenject;
using Base.GameLogic;
using Base.PLayer;
using Base.Services.SaveLoad;
using Base.Services.PersistentProgress;
using Base.Data;
using Base.Data.Game;

namespace Base.Services.Factories.UI
{
    public class GameSceneUIFactory : MonoBehaviour
    {
        [SerializeField] private BattleEndSetup _battleEndSetup;
        [SerializeField] private PauseMenuSetup _pauseMenuSetup;
        [SerializeField] private SettingsMenuSetup _settingsMenuSetup;

        [SerializeField] private UIWindowController _cannonsHUD;
        [SerializeField] private UIWindowController _shootMinigameUI;
        [SerializeField] private UIWindowController _pauseWindow;
        [SerializeField] private UIWindowController _settingsWindow;
        [SerializeField] private UIWindowController _battleEndWindow;

        [SerializeField] private ButtonClickHandler _launchShootMinigameButton;
        [SerializeField] private ButtonClickHandler _pauseButton;
        [SerializeField] private ButtonClickHandler _shootButton;
        [SerializeField] private ButtonClickHandler _openSettingsButton;
        [SerializeField] private ButtonClickHandler _resumeButton;
        [SerializeField] private ButtonClickHandler _closeSettingsWindowButton;

        private TimeController _timeController;
        private SceneChanger _sceneChanger;
        private GameUIStateMachine _uiStateMachine;
        private IAudioVolumeControllerService _volumeControllerService;
        private ISaveLoadService _saveLoadService;
        private IPersisentDataService _dataService;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Init(GameStateMachine gameStateMachine, TimeController timeController, SceneChanger sceneChanger,
            IAudioVolumeControllerService volumeControllerService, ISaveLoadService saveLoadService, 
            IPersisentDataService dataService)
        {
            _gameStateMachine = gameStateMachine;
            _timeController = timeController;
            _sceneChanger = sceneChanger;
            _volumeControllerService = volumeControllerService;
            _saveLoadService = saveLoadService;
            _dataService = dataService;
        }

        private void Awake()
        {
            _timeController.Resume();
            CreateUIStateMachine();
            Debug.Log("GameSceneUIFactory awakened");
        }

        private void OnEnable()
        {
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

        public BattleEndModel GetBattleEndModel(Infrastructure.Game game, Wallet wallet, StageInfo stageInfo, PlayerScore score)
        {
            return _battleEndSetup.Create(game, wallet, score, _saveLoadService, stageInfo.WinReward, stageInfo.DefeatReward);
        }

        private void CreateUIStateMachine()
        {
            _uiStateMachine = new GameUIStateMachine(_cannonsHUD, _shootMinigameUI,
                _pauseWindow, _settingsWindow, _battleEndWindow);

            _pauseMenuSetup.CreatePauseMenu(_sceneChanger);
            _settingsMenuSetup.CreateModel(_volumeControllerService, _saveLoadService, _dataService.GameData.AudioVolumeSettings);

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
