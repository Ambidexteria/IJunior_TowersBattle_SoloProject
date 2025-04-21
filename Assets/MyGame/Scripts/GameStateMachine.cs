using System;
using System.Collections.Generic;

namespace Base
{
    internal class GameStateMachine
    {
        private Game _game;
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(Game game)
        {
            _game = game;

            CreateStates();
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();

            IState state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }

        private void CreateStates()
        {
            _states = new Dictionary<Type, IState>
            {
                { typeof(BootstrapState), new BootstrapState() }
            };
        }
    }
}