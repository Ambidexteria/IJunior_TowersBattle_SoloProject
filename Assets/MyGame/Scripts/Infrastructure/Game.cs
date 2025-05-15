using Base.Data;
using Base.Data.Scenes;
using Base.Services.Factories.UI;
using UnityEngine;
using System;

namespace Base.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUIFactory _uIFactory;
        private MainMenuSceneData _mainMenuSceneData;
        private GameSceneData _gameSceneData;

        public Game(GameStateMachine gameStateMachine, IUIFactory uIFactory)
        {
            _gameStateMachine = gameStateMachine ?? throw new NullReferenceException(nameof(GameStateMachine));
            _uIFactory = uIFactory ?? throw new NullReferenceException(nameof(UIFactory));
            _uIFactory.Created += OnUICreated;

            _mainMenuSceneData = new(SceneNames.MainMenu, "UI/MainMenuUI");
            _gameSceneData = new(SceneNames.Game, "UI/PlayerHUD");
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

        private void OnUICreated(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out MainMenuUISetup setup))
                setup.GetModel().StartingBattle += StartBattle;
        }
    }
}