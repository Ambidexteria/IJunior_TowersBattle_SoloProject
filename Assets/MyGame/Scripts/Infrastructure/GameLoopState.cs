using Base.Services.PluginYG;
using UnityEngine;

namespace Base.Infrastructure
{
    internal class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine) 
        {
            ExceptionsTest.NullRefConstructorTest(nameof(GameLoopState), stateMachine);

            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}