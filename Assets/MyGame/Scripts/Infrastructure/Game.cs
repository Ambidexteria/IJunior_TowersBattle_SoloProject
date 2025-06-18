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
            ExceptionsTest.NullRefConstructorTest(nameof(Game), gameStateMachine, input);

            _gameStateMachine = gameStateMachine;

            _mainMenuSceneData = new(SceneNames.MainMenu, "UI/MainMenuUI");
            _gameSceneData = new(SceneNames.Game, "UI/PlayerHUD");

            _playerInputController = new PlayerInputController(input);
            _playerInputController.Enable();
        }

        public void EnterBootstrapState()
        {
            _gameStateMachine.Enter<BootstrapState, SceneData>(_mainMenuSceneData);
        }

        public void LoadGameScene()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneData>(_gameSceneData);
        }

        public void LoadMainMenu()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneData>(_mainMenuSceneData);
        }
    }
}