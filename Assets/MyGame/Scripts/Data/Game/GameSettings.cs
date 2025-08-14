using System;

namespace Base.Data.Game
{
    [Serializable]
    public class GameSettings
    {
        public AudioVolumeSettings AudioVolumeSettings;
        public string Language;
        public bool TutorialEnabled;
        public int CameraPosition;

        public GameSettings()
        {
            AudioVolumeSettings = new();
            Language = "ru";
            TutorialEnabled = true;
            CameraPosition = 0;
        }
    }
}
