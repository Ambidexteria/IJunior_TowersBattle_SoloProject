using Base.Logic;
using Base.Services.AssetManagment;
using Base.Services.Factories.Game;
using Base.Services.Factories.UI;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using Base.Services.SceneManagment;
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
        public GameStateMachine(LoadingCurtain loadingCurtain, SceneLoader sceneLoader, IGameFactory gameFactory, 
            IPersisentProgressService persisentProgress, ISaveLoadService saveLoadService, AssetLoader assetLoader)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader) },
                { typeof(LoadLevelState), new LoadLevelState(loadingCurtain, this, sceneLoader, gameFactory, persisentProgress, assetLoader) },
                { typeof(LoadProgressState), new LoadProgressState(this, persisentProgress, saveLoadService)},
                { typeof(GameLoopState), new GameLoopState(this) }
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
}