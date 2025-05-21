using Base.Infrastructure;
using Base.Services.SceneManagment;
using Base.Services.TimeManagment;
using Base.UI.Game.StateMachine;
using Base.UI.PauseMenu;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;

namespace Base.Services.Factories.UI
{
    public class GameSceneUIFactory : MonoBehaviour
    {
        [SerializeField] private PauseMenuSetup _pauseMenuSetup;

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
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Init(GameStateMachine gameStateMachine, TimeController timeController, SceneChanger sceneChanger)
        {
            _gameStateMachine = gameStateMachine;
            _timeController = timeController;
            _sceneChanger = sceneChanger;
        }

        private void Awake()
        {
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

            var model = _pauseMenuSetup.CreatePauseMenu(_timeController, _sceneChanger);
            //model.Pause();

            return _uiStateMachine;
        }

        private void CreateUIStateMachine()
        {
            _uiStateMachine = new GameUIStateMachine(_cannonsHUD, _shootMinigameUI,
                _pauseWindow, _settingsWindow, _winMessage, _defeatMessage);

            _uiStateMachine.Enter<CannonsHUDState>();
        }

        private void OnLaunchShootMinigameButtonClicked()
        {
            _uiStateMachine.Enter<ShootMinigameState>();
        }

        private void OnPauseButtonClicked()
        {
            _uiStateMachine.Enter<PauseState>();
        }

        private void OnShootButtonCliked()
        {
            _uiStateMachine.Enter<CannonsHUDState>();
        }

        private void OnOpenSettingsButtonClicked()
        {
            _uiStateMachine.Enter<SettingsMenuState>();
        }

        private void OnResumeButtonClicked()
        {
            _uiStateMachine.Enter<CannonsHUDState>();
        }

        private void OnCloseSettingsButtonClicked()
        {
            _uiStateMachine.Enter<PauseState>();
        }
    }
}
