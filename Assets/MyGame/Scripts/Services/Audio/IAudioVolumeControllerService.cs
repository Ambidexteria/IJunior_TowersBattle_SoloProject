namespace Base.Services.Audio
{
    public interface IAudioVolumeControllerService : IService
    {
        void SetMasterVolume(float volume);
        void SetMusicVolume(float volume);
        void SetSoundsVolume(float volume);
        void ToggleMute(bool value);
    }
}