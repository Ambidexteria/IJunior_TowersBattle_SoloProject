using Base.Infrastructure;
using Base.Logic;
using Base.UI.MainMenu;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.UI.Controller.StateMachine
{
    public class UIStateMachine
    {
        private Dictionary<Type, IUIState> _states = new Dictionary<Type, IUIState>();

        private IUIState _activeState;

        public UIStateMachine(MainMenuUIModel mainMenuModel, LoadingCurtain loadingCurtain)
        {
            _states = new Dictionary<Type, IUIState>();
            _states.Add(typeof(MainMenuState), new MainMenuState(this, mainMenuModel));
            _states.Add(typeof(LoadingCurtainState), new LoadingCurtainState(loadingCurtain));
            _states.Add(typeof(GameState), new GameState());
        }

        public void Enter<TState>() where TState : IUIState
        {
            _activeState?.Exit();

            _activeState = _states[typeof(TState)];
            _activeState?.Enter();
        }
    }

    public class GameState : IUIState
    {
        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }

    public class IdleState : IUIState
    {
        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }

    public class LoadingCurtainState : IUIState
    {
        private LoadingCurtain _loadingCurtain;

        public LoadingCurtainState(LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            Debug.Log($"Showing curtain curtain");
            _loadingCurtain.Show();
        }

        public void Exit()
        {
            Debug.Log($"Hiding curtain");
            _loadingCurtain.Hide();
        }
    }

    public class MainMenuState : IUIState
    {
        private readonly UIStateMachine _UIStateMachineModel;
        private readonly MainMenuUIModel _mainMenuModel;

        public MainMenuState(UIStateMachine uIStateMachineModel, MainMenuUIModel mainMenuModel)
        {
            _UIStateMachineModel = uIStateMachineModel;
            _mainMenuModel = mainMenuModel;
        }

        public void Enter()
        {
            Debug.Log($"{nameof(MainMenuState)} - entered");
            _mainMenuModel.Enable();
        }

        public void Exit()
        {
            _mainMenuModel.Disable();
        }
    }

    public interface IUIState
    {
        void Enter();
        void Exit();
    }
}
