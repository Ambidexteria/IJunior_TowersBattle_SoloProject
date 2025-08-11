using Base.Data.Game;
using Base.Data.Scenes;
using Base.Services.AssetManagment;
using Base.Services.Localization;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using UnityEngine;
using YG;

namespace Base.Infrastructure
{
    internal class LoadProgressState : IPayloadedState<SceneData>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersisentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly InputService _input;
        private readonly ILocalizationService _localizationService;
        private readonly AssetLoader _assetLoader;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersisentDataService persisentProgressService,
            ISaveLoadService saveLoadService, InputService input, ILocalizationService localizationService,
            AssetLoader assetLoader)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(LoadProgressState), gameStateMachine, persisentProgressService,
                saveLoadService);

            _gameStateMachine = gameStateMachine;
            _progressService = persisentProgressService;
            _saveLoadService = saveLoadService;
            _input = input;
            _localizationService = localizationService;
            _assetLoader = assetLoader;
        }

        public void Enter(SceneData scene)
        {
            ExceptionsTest.NullRefMethodTest(nameof(LoadProgressState), nameof(Enter), scene);

            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, SceneData>(scene);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            if (_input.Debug.ResetProgress.IsPressed())
            {
                Debug.LogWarning($"ProgressDeleted");
                _progressService.GameData = CreateProgress();
            }
            else
            {
                _progressService.GameData = _saveLoadService.LoadProgress() ?? CreateProgress();
            }

            Debug.Log($"LANGUAGE = {YG2.lang}");
            //_localizationService.SetLanguage("tr");
            _localizationService.SetLanguage(YG2.lang);
            _progressService.GameData.GameSettings.Language = YG2.lang;
            _progressService.GameData.StagesData.CheckForUpdate();
        }

        private GameData CreateProgress()
        {
            Debug.Log("CREATING NEW PLAYER PROGRESS");
            return new GameData();
        }
    }
}