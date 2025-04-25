using Base.Data;
using Base.Services.SceneManagment;

namespace Base.Infrastructure
{
    internal class BootstrapState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader) 
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(SceneNames.Initial, EnterLoadLevelState);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevelState()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}