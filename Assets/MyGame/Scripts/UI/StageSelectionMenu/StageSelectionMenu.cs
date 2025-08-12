using Base.Data;
using Base.Data.Game;
using Base.Infrastructure;
using Base.Services.SaveLoad;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenu
    {
        private readonly StageIconModel[] _stagesIcons;
        private readonly StagesData _stagesData;
        private readonly GameSettings _gameSettings;
        private readonly ISaveLoadService _saveLoadService;
        private readonly Game _game;
        private StageIconModel _activeIcon;

        public StageSelectionMenu(StageIconModel[] iconModels, StagesData stagesData, GameSettings gameSettings, 
            ISaveLoadService saveLoadService, Game game)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(StageSelectionMenu), iconModels, stagesData, gameSettings,
                saveLoadService);

            _stagesIcons = iconModels;
            _stagesData = stagesData;
            _gameSettings = gameSettings;
            _saveLoadService = saveLoadService;
            _game = game;

            SubcribeForIconModels();
            _activeIcon = GetIconByName(_stagesData.GetSelectedStage().Name);
            _activeIcon.ShowBorder();
        }

        private void SubcribeForIconModels()
        {
            foreach (var iconModel in _stagesIcons)
            {
                iconModel.Choosed += OnStageChoosed;
            }
        }

        private void OnStageChoosed(string name)
        {
            if (_stagesData.IsStageExist(name))
            {
                _stagesData.SetSelectedStage(name);
                _saveLoadService.SaveProgress();
                _activeIcon.HideBorder();

                _activeIcon = GetIconByName(name);
                _activeIcon.ShowBorder();
                _game.LoadGameScene();
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
