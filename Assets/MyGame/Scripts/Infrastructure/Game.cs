using Base.Data;
using Base.Data.Scenes;
using Base.Services.Input;

namespace Base.Infrastructure
{
    public class Game
    {
        private readonly InputController _playerInputController;
        private readonly GameStateMachine _gameStateMachine;
        private readonly MainMenuSceneData _mainMenuSceneData;
        private readonly GameSceneData _gameSceneData;

        public Game(GameStateMachine gameStateMachine, InputService input)
        {
            _gameStateMachine = gameStateMachine;

            _mainMenuSceneData = new(SceneNames.MainMenu);
            _gameSceneData = new(SceneNames.Game);

            _playerInputController = new InputController(input);
            _playerInputController.Enable();
        }

        public void EnterBootstrapState()
        {
            _gameStateMachine.Enter<BootstrapState, SceneData>(_mainMenuSceneData);
        }

        public void LoadGameScene()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneData>(_gameSceneData);
        }

        public void LoadMainMenu()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneData>(_mainMenuSceneData);
        }
    }
}