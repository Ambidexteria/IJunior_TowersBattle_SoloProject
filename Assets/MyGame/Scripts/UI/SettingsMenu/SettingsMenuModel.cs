using Base.Data;
using Base.Services.Audio;
using Base.Services.SaveLoad;

namespace Base.UI.Settings
{
    public class SettingsMenuModel
    {
        private readonly IAudioVolumeControllerService _volumeController;
        private readonly ISaveLoadService _saveLoadService;
        private readonly AudioVolumeSettings _volumeSettings;

        public SettingsMenuModel(IAudioVolumeControllerService volumeController, ISaveLoadService saveLoadService,
            AudioVolumeSettings volumeSettings)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(SettingsMenuModel), volumeController, saveLoadService, volumeSettings);

            _volumeController = volumeController;
            _saveLoadService = saveLoadService;
            _volumeSettings = volumeSettings;
        }

        public void ToggleMute(bool value)
        {
            _volumeController.ToggleMute(value);
            _volumeSettings.Muted = value;
            SaveSettings();
        }

        public void SetMasterVolume(float volume)
        {
            _volumeController.SetMasterVolume(volume);
            _volumeSettings.MasterVolume = volume;
            SaveSettings();
        }

        public void SetMusicVolume(float volume)
        {
            _volumeController.SetMusicVolume(volume);
            _volumeSettings.MusicVolume = volume;
            SaveSettings();
        }

        public void SetSoundsVolume(float volume)
        {
            _volumeController.SetSoundsVolume(volume);
            _volumeSettings.SoundsVolume = volume;
            SaveSettings();
        }

        private void SaveSettings()
        {
            _saveLoadService.SaveProgress();
        }
    }
}
