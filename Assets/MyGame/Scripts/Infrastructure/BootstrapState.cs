using Base.Data;
using Base.Data.Scenes;
using Base.Services.PluginYG;
using Base.Services.SceneManagment;
using System.Collections;
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
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _coroutineRunner = coroutineRunner;
            _input = input;
        }

        public void Enter(SceneData sceneData)
        {
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
            while (YG2.isSDKEnabled == false)
                yield return null;

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
            _gameStateMachine.Enter<LoadProgressState, SceneData>(_currentSceneData);
        }
    }
}