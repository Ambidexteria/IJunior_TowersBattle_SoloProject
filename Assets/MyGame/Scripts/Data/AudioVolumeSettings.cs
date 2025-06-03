using System;

namespace Base.Data
{
    [Serializable]
    public class AudioVolumeSettings
    {
        public float MasterVolume = 1f;
        public float SoundsVolume = 1f;
        public float MusicVolume = 1f;
        public bool Muted = false;
    }
}
