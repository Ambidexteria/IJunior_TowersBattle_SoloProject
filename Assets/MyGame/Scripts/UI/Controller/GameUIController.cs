using Base.Logic;
using Base.Services.Factories.UI;
using Base.UI.Game.StateMachine;
using Base.UI.MainMenu;
using System;
using UnityEngine;

namespace Base.UI.Controller
{
    public class GameUIController
    {
        private readonly GameUIStateMachine _gameUIStateMachine;

        public event Action StartingBattle;

        public GameUIController()
        {

        }

        public void Enable()
        {
        }

        public void ShowLoadingCurtain()
        {
            Debug.Log("showing loading curtain");
        }

        public void ShowMainMenu()
        {
        }

        public void ShowPlayerHUD()
        {
        }

        private void OnStartingBattle()
        {
            //_stateMachine.Enter<MainMenuState>();
            StartingBattle?.Invoke();
        }
    }
}
