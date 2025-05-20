
using UnityEngine;

namespace Base.Infrastructure
{
    internal class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine) 
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            //Debug.Log($"{nameof(GameLoopState)} - entered");
            //_uiController.ShowMainMenu();
        }

        public void Exit()
        {

        }
    }
}