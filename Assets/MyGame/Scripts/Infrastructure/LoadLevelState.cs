using Zenject;
using Base.Services.SceneManagment;
using Base.Data;
using Base.Services.PersistentProgress;
using Base.Services.Factories.Game;
using Base.UI.Controller;

namespace Base.Infrastructure
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        private readonly UIController _uiController;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersisentProgressService _progressService;

        [Inject]
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, UIController uiStateMachine, 
            IGameFactory gameFactory, IPersisentProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _uiController = uiStateMachine;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _uiController.ShowLoadingCurtain();
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        public void Exit()
        {

        }

        private void OnLoaded()
        {
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
            _uiController.ShowMainMenu();
        }

        private void InformProgressReaders()
        {
            foreach(ISavedProgressReader progressReader in _gameFactory.GetProgressReaders())
            {
                progressReader.LoadProgress(_progressService.PlayerProgress);
            }
        }
    }
}