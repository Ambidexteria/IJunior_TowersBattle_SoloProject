using Base.Data;
using Base.Data.Scenes;
using Base.Services.PluginYG;
using Base.Services.SceneManagment;
using System.Collections;
using UnityEngine;
using YG;

namespace Base.Infrastructure
{
    internal class BootstrapState : IPayloadedState<SceneData>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly InputService _input;
        private readonly GameStateMachine _gameStateMachine;

        private SceneData _currentSceneData;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ICoroutineRunner coroutineRunner, InputService input)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(BootstrapState), gameStateMachine, sceneLoader, coroutineRunner);

            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _coroutineRunner = coroutineRunner;
            _input = input;
        }

        public void Enter(SceneData sceneData)
        {
            ExceptionsTest.NullRefMethodTest(nameof(BootstrapState), nameof(Enter), sceneData);

            _currentSceneData = sceneData;
            _sceneLoader.LoadScene(SceneNames.Initial, EnsureYandexSDKInitialized);
        }

        public void Exit()
        {

        }

        private void EnsureYandexSDKInitialized()
        {
            _coroutineRunner.LaunchCoroutine(StartEnsureYandexSDKInitializedCoroutine());
        }

        private IEnumerator StartEnsureYandexSDKInitializedCoroutine()
        {
            bool enabled = false;

            while (enabled == false || YG2.isSDKEnabled == false)
            {
                yield return null;

                if (_input.Debug.ContinueLoading.IsPressed())
                    enabled = true;
            }

            SendMetrics();
            EnterLoadProgressState();
        }

        private void SendMetrics()
        {
            if (YG2.isFirstGameSession)
                MetricsService.CallFirstLaunchEvent();

            MetricsService.CallGameLaunchedEvent();
        }

        private void EnterLoadProgressState()
        {
            Debug.Log("Entering LoadProgressState");
            _gameStateMachine.Enter<LoadProgressState, SceneData>(_currentSceneData);
        }
    }
}