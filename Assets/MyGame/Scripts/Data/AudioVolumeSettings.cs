using System;

namespace Base.Data
{
    [Serializable]
    public class AudioVolumeSettings
    {
        public float MasterVolume;
        public float SoundsVolume;
        public float MusicVolume;
        public bool Muted;
    }
}
