using Base.Services.Audio;

namespace Base.UI.Settings
{
    public class SettingsMenuModel
    {
        private readonly AudioVolumeControllerService _volumeController;

        public SettingsMenuModel(AudioVolumeControllerService volumeController)
        {
            _volumeController = volumeController;
        }

        public void ToggleMute(bool value)
        {
            _volumeController.ToggleMute(value);
        }

        public void SetMasterVolume(float volume)
        {
            _volumeController.SetMasterVolume(volume);
        }

        public void SetMusicVolume(float volume)
        {
            _volumeController.SetMusicVolume(volume);
        }

        public void SetSoundsVolume(float volume)
        {
            _volumeController.SetSoundsVolume(volume);
        }
    }
}
