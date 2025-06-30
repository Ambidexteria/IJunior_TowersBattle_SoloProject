using Base.Data;
using Base.Data.Game;
using Base.Services.SaveLoad;
using System;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenu
    {
        private readonly StageIconModel[] _stagesIcons;
        private readonly StagesData _stagesData;
        private readonly GameSettings _gameSettings;
        private readonly ISaveLoadService _saveLoadService;
        private StageIconModel _activeIcon;

        public StageSelectionMenu(StageIconModel[] iconModels, StagesData stagesData, GameSettings gameSettings, ISaveLoadService saveLoadService)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(StageSelectionMenu), iconModels, stagesData, gameSettings,
                saveLoadService);

            _stagesIcons = iconModels;
            _stagesData = stagesData;
            _gameSettings = gameSettings;
            _saveLoadService = saveLoadService;

            SubcribeForIconModels();
            _activeIcon = GetIconByName(_stagesData.GetSelectedStage().Name);
            _activeIcon.ShowBorder();
        }

        public event Action<string> StageSelected;

        private void SubcribeForIconModels()
        {
            foreach (var iconModel in _stagesIcons)
            {
                iconModel.Choosed += OnStageChoosed;
            }
        }

        private void OnStageChoosed(string name)
        {
            if (_stagesData.GetSelectedStage().Name == name)
                return;

            if (_stagesData.IsStageExist(name))
            {
                _stagesData.SetSelectedStage(name);
                _saveLoadService.SaveProgress();
                _activeIcon.HideBorder();

                _activeIcon = GetIconByName(name);
                _activeIcon.ShowBorder();
            }
        }

        private StageIconModel GetIconByName(string name)
        {
            StageIconModel iconModel = null;

            foreach (var icon in _stagesIcons)
            {
                if (icon.Name == name)
                {
                    iconModel = icon;
                    break;
                }
            }

            return iconModel;
        }
    }
}
