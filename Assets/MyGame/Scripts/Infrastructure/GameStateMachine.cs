using Base.Logic;
using Base.Services.Factories;
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
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, GameFactory gameFactory, 
            IPersisentProgressService persisentProgress, ISaveLoadService saveLoadService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader) },
                { typeof(LoadLevelState), new LoadLevelState(this, sceneLoader, loadingCurtain, gameFactory) },
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