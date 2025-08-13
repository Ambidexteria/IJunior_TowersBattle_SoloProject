using Base.Services.SceneManagment;
using Base.Services.PersistentProgress;
using Base.Data.Scenes;
using Base.Services.SaveLoad;

namespace Base.Infrastructure
{
    internal class LoadLevelState : IPayloadedState<SceneData>
    {
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersisentDataService _progressService;

        private SceneData _currentSceneData;

        public LoadLevelState(LoadingCurtain loadingCurtain, GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            ISaveLoadService saveLoadService, IPersisentDataService progressService)
        {
            _progressService = progressService;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _saveLoadService = saveLoadService;
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