using Base.Data;
using Base.Data.Scenes;
using UnityEngine;
using System;
using Base.Services.Input;

namespace Base.Infrastructure
{
    public class Game
    {
        private readonly PlayerInputController _playerInputController;
        private readonly GameStateMachine _gameStateMachine;
        private MainMenuSceneData _mainMenuSceneData;
        private GameSceneData _gameSceneData;

        public Game(GameStateMachine gameStateMachine, InputService input)
        {
            _gameStateMachine = gameStateMachine ?? throw new NullReferenceException(nameof(GameStateMachine));

            _mainMenuSceneData = new(SceneNames.MainMenu, "UI/MainMenuUI");
            _gameSceneData = new(SceneNames.Game, "UI/PlayerHUD");
            _playerInputController = new PlayerInputController(input);
            _playerInputController.Enable();
        }

        public void EnterBootstrapState()
        {
            _gameStateMachine.Enter<BootstrapState, SceneData>(_mainMenuSceneData);
        }

        private void StartBattle()
        {
            Debug.Log($"{nameof(Game)} - {nameof(StartBattle)}");
            _gameStateMachine.Enter<LoadLevelState, SceneData>(_gameSceneData);
        }

        private void OnSceneLoaded(Canvas gameObject)
        {
            if (gameObject.TryGetComponent(out MainMenuUISetup setup))
                setup.GetModel().StartingBattle += StartBattle;
        }
    }
}