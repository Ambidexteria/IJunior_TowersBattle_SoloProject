using Base.Logic;
using Base.Services.Factories;
using Base.Services.Factories.UI;
using Base.UI.Controller.StateMachine;
using Base.UI.MainMenu;
using System;
using UnityEngine;

namespace Base.UI.Controller
{
    public class UIController
    {
        private UIStateMachine _stateMachine;
        private MainMenuUIModel _mainMenuModel;
        private readonly IUIFactory _uIFactory;

        public event Action StartingBattle;

        public UIController(IUIFactory uIFactory, LoadingCurtain loadingCurtain)
        {
            _uIFactory = uIFactory;
            _mainMenuModel = _uIFactory.CreateMainMenuModel();
            _stateMachine = new UIStateMachine(_mainMenuModel, loadingCurtain);

            Enable();
        }

        public void Enable()
        {
            _mainMenuModel.StartingBattle += OnStartingBattle;
        }

        public void ShowLoadingCurtain()
        {
            Debug.Log("showing loading curtain");
            _stateMachine.Enter<LoadingCurtainState>();
        }

        public void ShowMainMenu()
        {
            _stateMachine.Enter<MainMenuState>();
        }

        public void ShowPlayerHUD()
        {
            _stateMachine.Enter<GameState>();
        }

        private void OnStartingBattle()
        {
            //_stateMachine.Enter<MainMenuState>();
            StartingBattle?.Invoke();
        }
    }
}
