using UnityEngine;
using Base.Logic;
using Base.Services.Factories;
using Zenject;
using Base.Services.SceneManagment;
using System;
using Base.Data;
using Base.Services.PersistentProgress;

namespace Base.Infrastructure
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersisentProgressService _progressService;

        [Inject]
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersisentProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
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
            _gameFactory.CreateHUD();

            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();


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