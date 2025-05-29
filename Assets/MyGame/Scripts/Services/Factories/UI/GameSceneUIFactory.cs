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
using System;
using Base.PLayer;
using Base.Services.SaveLoad;

namespace Base.Services.Factories.UI
{
    public class GameSceneUIFactory : MonoBehaviour
    {
        [SerializeField] private BattleEndSetup _battleEndSetup;
        [SerializeField] private PauseMenuSetup _pauseMenuSetup;
        [SerializeField] private SettingsMenuSetup _settingsMenuSetup;

        [SerializeField] private UIWindowController _cannonsHUD;
        [SerializeField] private UIWindowController _playerCannonHUD;
        [SerializeField] private UIWindowController _npcCannonHUD;
        [SerializeField] private UIWindowController _shootMinigameUI;
        [SerializeField] private UIWindowController _pauseWindow;
        [SerializeField] private UIWindowController _settingsWindow;
        [SerializeField] private UIWindowController _winMessage;
        [SerializeField] private UIWindowController _defeatMessage;

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

        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Init(GameStateMachine gameStateMachine, TimeController timeController, SceneChanger sceneChanger,
            IAudioVolumeControllerService volumeControllerService)
        {
            _gameStateMachine = gameStateMachine;
            _timeController = timeController;
            _sceneChanger = sceneChanger;
            _volumeControllerService = volumeControllerService;
        }

        private void Awake()
        {
            _timeController.Resume();
            CreateUIStateMachine();
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

        public BattleEndModel GetBattleEndModel(Infrastructure.Game game, Wallet wallet, ISaveLoadService saveLoadService)
        {
            return _battleEndSetup.Create(game, wallet, saveLoadService);
        }

        private void CreateUIStateMachine()
        {
            _uiStateMachine = new GameUIStateMachine(_cannonsHUD, _shootMinigameUI,
                _pauseWindow, _settingsWindow, _winMessage, _defeatMessage);

            _pauseMenuSetup.CreatePauseMenu(_sceneChanger);
            _settingsMenuSetup.CreateModel(_volumeControllerService);

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
