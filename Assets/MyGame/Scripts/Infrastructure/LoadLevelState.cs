using UnityEngine;
using Base.Logic;
using Base.Services.Factories;
using Zenject;
using Base.Services.SceneManagment;

namespace Base.Infrastructure
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly GameFactory _gameFactory;

        [Inject]
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            Debug.Log("");
            _gameFactory.CreateHUD();
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}