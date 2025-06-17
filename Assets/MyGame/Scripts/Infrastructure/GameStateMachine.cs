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
        public GameStateMachine(LoadingCurtain loadingCurtain, SceneLoader sceneLoader,
            IPersisentDataService persisentProgress, ISaveLoadService saveLoadService, AssetLoader assetLoader,
            ICoroutineRunner coroutineRunner)
        {
            ExceptionsTest.NullRefTest(nameof(GameStateMachine), "constructor", loadingCurtain, sceneLoader,
             persisentProgress, saveLoadService, assetLoader,
             coroutineRunner);

            _states = new Dictionary<Type, IExitableState>
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader, coroutineRunner) },
                { typeof(LoadLevelState), new LoadLevelState(loadingCurtain, this, sceneLoader, saveLoadService, persisentProgress) },
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
            ExceptionsTest.NullRefTest(nameof(GameStateMachine), nameof(Enter), payload);

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