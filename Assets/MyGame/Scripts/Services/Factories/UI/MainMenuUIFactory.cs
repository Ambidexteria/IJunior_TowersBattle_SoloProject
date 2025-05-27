using Base.Services.TimeManagment;
using Base.UI.StateMachine;
using UnityEngine;
using Zenject;

namespace Base.Services.Factories.UI
{
    public class MainMenuUIFactory : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _startBattleButton;

        [SerializeField] private ButtonClickHandler _openStagesButton;
        [SerializeField] private ButtonClickHandler _openShopButton;
        [SerializeField] private ButtonClickHandler _openSettingsButton;

        [SerializeField] private UIWindowController _mainButtons;
        [SerializeField] private UIWindowController _stages;
        [SerializeField] private UIWindowController _shop;
        [SerializeField] private UIWindowController _settings;

        [SerializeField] private ButtonClickHandler _closeStagesButton;
        [SerializeField] private ButtonClickHandler _closeShopButton;
        [SerializeField] private ButtonClickHandler _closeSettingsButton;

        private TimeController _timeController;

        private MainMenuUIStateMachine _stateMachine;

        [Inject]
        private void Init(TimeController timeController)
        {
            _timeController = timeController;
        }

        private void Awake()
        {
            _timeController.SetDefaultTimeScale();
            CreateUIStateMachine();
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
            _stateMachine = new MainMenuUIStateMachine(_mainButtons, _shop, _stages, _settings);
            _stateMachine.Enter<MainMenuState>();
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
