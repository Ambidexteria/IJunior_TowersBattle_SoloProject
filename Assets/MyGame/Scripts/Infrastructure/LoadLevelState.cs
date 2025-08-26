using Base.Data.Scenes;
using Base.Services.SceneManagment;

namespace Base.Infrastructure
{
    internal class LoadLevelState : IPayloadedState<SceneData>
    {
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        private SceneData _currentSceneData;

        public LoadLevelState(LoadingCurtain loadingCurtain, GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(SceneData sceneData)
        {
            _loadingCurtain.Show();
            _currentSceneData = sceneData;
            _sceneLoader.LoadScene(_currentSceneData.SceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}