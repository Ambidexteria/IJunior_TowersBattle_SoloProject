using Base.Data;
using Base.Data.Scenes;
using Base.Infrastructure;

namespace Base.Services.SceneManagment
{
    public class SceneChanger
    {
        private readonly GameStateMachine _gameStateMachine;

        public SceneChanger(GameStateMachine gaStateMachine)
        {
            _gameStateMachine = gaStateMachine;
        }

        public void ReturnToMainMenu()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneData>(new MainMenuSceneData(SceneNames.MainMenu, ""));
        }

        public void ReloadGameScene()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneData>(new GameSceneData(SceneNames.Game, ""));
        }
    }
}
