using System;

namespace Base.Data.Game
{
    [Serializable]
    public class GameSettings
    {
        public StageInfo SelectedStage;

        public GameSettings ()
        {
        }

        public GameSettings(StageInfo defaultStage)
        {
            SelectedStage = defaultStage;
        }
    }
}
