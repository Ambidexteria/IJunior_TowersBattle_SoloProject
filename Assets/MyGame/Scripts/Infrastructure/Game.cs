using Base.Data;
using Base.UI;
using Base.UI.Controller;
using Base.UI.MainMenu;
using System;
using UnityEngine;

namespace Base.Infrastructure
{
    public class Game
    {
        private GameStateMachine _gameStateMachine;
        private UIController _uiController;

        public GameStateMachine GameStateMachine => _gameStateMachine;

        public Game(GameStateMachine gameStateMachine, UIController uIControllerModel)
        {
            _uiController = uIControllerModel;
            _gameStateMachine = gameStateMachine;

            _uiController.StartingBattle += StartBattle;
        }

        private void StartBattle()
        {
            Debug.Log($"{nameof(Game)} - {nameof(StartBattle)}");
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.Game);
        }
    }
}