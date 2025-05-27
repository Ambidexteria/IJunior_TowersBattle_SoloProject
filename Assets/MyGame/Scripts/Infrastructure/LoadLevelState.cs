using Base.Services.SceneManagment;
using Base.Data;
using Base.Services.PersistentProgress;
using Base.Services.Factories.Game;
using Base.Data.Scenes;
using Base.Services.AssetManagment;
using Base.Logic;
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

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName, OnLoaded);
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
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            _saveLoadService.LoadProgress();
        }
    }
}