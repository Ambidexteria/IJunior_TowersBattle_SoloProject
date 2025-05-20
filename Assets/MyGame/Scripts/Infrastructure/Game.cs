using Base.Data;
using Base.Data.Scenes;
using Base.Services.Factories.UI;
using UnityEngine;
using System;
using Base.Services.Input;

namespace Base.Infrastructure
{
    public class Game
    {
        private readonly PlayerInputController _playerInputController;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUIFactory _uIFactory;
        private MainMenuSceneData _mainMenuSceneData;
        private GameSceneData _gameSceneData;

        public Game(GameStateMachine gameStateMachine, IUIFactory uIFactory, InputService input)
        {
            _gameStateMachine = gameStateMachine ?? throw new NullReferenceException(nameof(GameStateMachine));
            _uIFactory = uIFactory ?? throw new NullReferenceException(nameof(UIFactory));
            _uIFactory.Created += OnSceneLoaded;

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