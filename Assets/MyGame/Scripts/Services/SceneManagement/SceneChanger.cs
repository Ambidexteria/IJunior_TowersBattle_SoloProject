using Base.Data;
using Base.Data.Scenes;
using Base.Infrastructure;
using System;

namespace Base.Services.SceneManagment
{
    public class SceneChanger
    {
        private readonly GameStateMachine _gameStateMachine;

        public SceneChanger(GameStateMachine gameStateMachine)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(SceneChanger), gameStateMachine);
            _gameStateMachine = gameStateMachine;
        }

        public event Action ChangingScene;

        public void ReturnToMainMenu()
        {
            ChangingScene?.Invoke();
            _gameStateMachine.Enter<LoadLevelState, SceneData>(new MainMenuSceneData(SceneNames.MainMenu));
        }

        public void ReloadGameScene()
        {
            ChangingScene?.Invoke();
            _gameStateMachine.Enter<LoadLevelState, SceneData>(new GameSceneData(SceneNames.Game));
        }
    }
}
