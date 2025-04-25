using Base.Infrastructure;
using UnityEngine;
using Zenject;

namespace Base.Infrastructure
{
    public class Game
    {
        private GameStateMachine _gameStateMachine;

        public GameStateMachine GameStateMachine => _gameStateMachine;

        public Game()
        {
            Debug.Log("Game constructed");
        }

        [Inject]
        private void Init(GameStateMachine gameStateMachine)
        {
            Debug.Log("Game initiated");
            _gameStateMachine = gameStateMachine;
        }
    }
}