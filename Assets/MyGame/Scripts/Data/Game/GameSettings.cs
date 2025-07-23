using System;

namespace Base.Data.Game
{
    [Serializable]
    public class GameSettings
    {
        public AudioVolumeSettings AudioVolumeSettings;
        public string Language;
        public bool TutorialEnabled;

        public GameSettings()
        {
            AudioVolumeSettings = new();
            Language = "ru";
            TutorialEnabled = true;
        }
    }
}
