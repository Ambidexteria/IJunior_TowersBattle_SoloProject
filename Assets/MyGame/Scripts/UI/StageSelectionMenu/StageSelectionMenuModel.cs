using Base.Data;
using Base.Data.Game;
using System;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuModel
    {
        private readonly StagesData _stagesData;
        private readonly GameSettings _gameSettings;

        public StageSelectionMenuModel(StagesData stagesData, GameSettings gameSettings)
        {
            _stagesData = stagesData;
            _gameSettings = gameSettings;
        }

        public event Action<string> StageSelected;

        public void SetActiveStage(string name)
        {
            if (_stagesData.TryGetStageByName(name, out StageInfo stageInfo))
            {
                _gameSettings.SelectedStage = stageInfo;
                StageSelected?.Invoke(name);
            }
        }
    }
}
