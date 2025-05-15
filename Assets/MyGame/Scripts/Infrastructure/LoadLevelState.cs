using Base.Services.SceneManagment;
using Base.Data;
using Base.Services.PersistentProgress;
using Base.Services.Factories.Game;
using Base.Services.Factories.UI;
using Base.Data.Scenes;
using Base.Services.AssetManagment;
using Base.Logic;

namespace Base.Infrastructure
{
    internal class LoadLevelState : IPayloadedState<SceneData>
    {
        private readonly LoadingCurtain loadingCurtain;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersisentProgressService _progressService;
        private readonly AssetLoader assetLoader;

        private SceneData _currentSceneData;

        public LoadLevelState(LoadingCurtain loadingCurtain, GameStateMachine gameStateMachine, SceneLoader sceneLoader, IUIFactory uIFactory,
            IGameFactory gameFactory, IPersisentProgressService progressService, AssetLoader assetLoader)
        {
            _uiFactory = uIFactory;
            _gameFactory = gameFactory;
            _progressService = progressService;
            this.assetLoader = assetLoader;
            this.loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        public void Enter(SceneData sceneData)
        {
            loadingCurtain.Show();
            _currentSceneData = sceneData;
            _sceneLoader.LoadScene(_currentSceneData.SceneName, OnLoaded);
        }

        public void Exit()
        {
            loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InformProgressReaders();

            if (_currentSceneData != null)
                _uiFactory.CreateUI(_currentSceneData.UIName);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.GetProgressReaders())
            {
                progressReader.LoadProgress(_progressService.PlayerProgress);
            }
        }
    }
}