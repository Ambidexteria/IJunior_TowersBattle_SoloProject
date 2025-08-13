using Base.Services.AssetManagment;
using Base.Services.Localization;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using Base.Services.SceneManagment;
using System;
using System.Collections.Generic;

namespace Base.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(LoadingCurtain loadingCurtain, SceneLoader sceneLoader,
            IPersisentDataService persisentProgress, ISaveLoadService saveLoadService, AssetLoader assetLoader,
            ICoroutineRunner coroutineRunner, InputService input, ILocalizationService localizationService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader, coroutineRunner, input) },
                { typeof(LoadLevelState), new LoadLevelState(loadingCurtain, this, sceneLoader, saveLoadService, persisentProgress) },
                { typeof(LoadProgressState), new LoadProgressState(this, persisentProgress, saveLoadService, localizationService)},
                { typeof(GameLoopState), new GameLoopState(this) }
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();

            IState state = ConvertState<TState>();
            _activeState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
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