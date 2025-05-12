using Base.Data;
using Base.Logic;
using Base.Services.Factories.Game;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using Base.Services.SceneManagment;
using Base.UI;
using Base.UI.Controller;
using Base.UI.Controller.StateMachine;
using Base.UI.MainMenu;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Base.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public GameStateMachine(SceneLoader sceneLoader, UIController uiController, IGameFactory gameFactory, 
            IPersisentProgressService persisentProgress, ISaveLoadService saveLoadService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader) },
                { typeof(LoadMainMenuState), new LoadMainMenuState(this, sceneLoader, uiController) },
                { typeof(LoadLevelState), new LoadLevelState(this, sceneLoader, uiController, gameFactory, persisentProgress) },
                { typeof(LoadProgressState), new LoadProgressState(this, persisentProgress, saveLoadService)},
                { typeof(GameLoopState), new GameLoopState(this, uiController) }
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            Debug.Log($"Enter {nameof(TState)} state");
            _activeState?.Exit();

            IState state = ConvertState<TState>();
            _activeState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            Debug.Log($"Enter {nameof(TState)} state with payload {nameof(TPayload)}");
            _activeState?.Exit();

            IPayloadedState<TPayload> state = ConvertState<TState>();
            _activeState = state;
            state.Enter(payload);
        }

        private TState ConvertState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }

    public class LoadMainMenuState : IState
    {
        private const string MainMenuScene = SceneNames.MainMenu;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly UIController _uIController;

        public LoadMainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, UIController uIController)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uIController = uIController;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(MainMenuScene, OnMainMenuLoaded);
            _uIController.ShowLoadingCurtain();
        }

        public void Exit()
        {

        }

        private void OnMainMenuLoaded()
        {
            _gameStateMachine.Enter<GameLoopState>();
            _uIController.ShowMainMenu();
        }
    }
}