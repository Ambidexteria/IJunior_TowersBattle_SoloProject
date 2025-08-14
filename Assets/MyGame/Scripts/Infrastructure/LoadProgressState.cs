using Base.Data.Game;
using Base.Data.Scenes;
using Base.Services.Localization;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using YG;

namespace Base.Infrastructure
{
    internal class LoadProgressState : IPayloadedState<SceneData>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersisentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILocalizationService _localizationService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersisentDataService persisentProgressService,
            ISaveLoadService saveLoadService, ILocalizationService localizationService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = persisentProgressService;
            _saveLoadService = saveLoadService;
            _localizationService = localizationService;
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
            _progressService.GameData = _saveLoadService.LoadProgress() ?? CreateProgress();

            _localizationService.SetLanguage(YG2.lang);
            _progressService.GameData.GameSettings.Language = YG2.lang;
            _progressService.GameData.StagesData.CheckForUpdate();
        }

        private GameData CreateProgress()
        {
            return new GameData();
        }
    }
}