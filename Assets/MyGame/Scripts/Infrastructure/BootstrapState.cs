using Base.Data;
using Base.Data.Scenes;
using Base.Services.SceneManagment;

namespace Base.Infrastructure
{
    internal class BootstrapState : IPayloadedState<SceneData>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;

        private SceneData _currentSceneData;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader) 
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(SceneData sceneData)
        {
            _currentSceneData = sceneData;
            _sceneLoader.LoadScene(SceneNames.Initial, EnterLoadLevelState);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevelState()
        {
            _gameStateMachine.Enter<LoadProgressState, SceneData>(_currentSceneData);
        }
    }
}