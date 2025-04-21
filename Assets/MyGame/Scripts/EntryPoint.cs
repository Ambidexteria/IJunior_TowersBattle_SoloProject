using UnityEngine;
using Zenject;

namespace Base
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ProjectContext _projectContext;

        private Game _game;
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _game = new Game();
            _gameStateMachine = new GameStateMachine(_game);

            _gameStateMachine.Enter<BootstrapState>();
        }
    }

    public interface IState
    {
        void Enter();
        void Exit();
    }
}
