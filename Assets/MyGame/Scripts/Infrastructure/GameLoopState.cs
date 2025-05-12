
using Base.UI.Controller;
using UnityEngine;

namespace Base.Infrastructure
{
    internal class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIController _uiController;

        public GameLoopState(GameStateMachine stateMachine, UIController uiController) 
        {
            _stateMachine = stateMachine;
            _uiController = uiController;
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