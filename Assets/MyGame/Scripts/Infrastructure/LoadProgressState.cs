using Base.Data;
using Base.Data.Scenes;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;

namespace Base.Infrastructure
{
    internal class LoadProgressState : IPayloadedState<SceneData>
    {
        private GameStateMachine _gameStateMachine;
        private IPersisentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersisentProgressService persisentProgressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = persisentProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter(SceneData scene)
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, SceneData>(scene);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? CreateProgress();
        }

        private PlayerProgress CreateProgress()
        {
            return new PlayerProgress(SceneNames.MainMenu);
        }
    }
}