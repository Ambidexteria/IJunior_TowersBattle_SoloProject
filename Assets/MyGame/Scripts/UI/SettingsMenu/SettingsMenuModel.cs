using Base.Data;
using Base.Services.Audio;
using Base.Services.Localization;
using Base.Services.SaveLoad;
using System.Runtime.CompilerServices;

namespace Base.UI.Settings
{
    public class SettingsMenuModel
    {
        private readonly IAudioVolumeControllerService _volumeController;
        private readonly ISaveLoadService _saveLoadService;
        private readonly AudioVolumeSettings _volumeSettings;
        private readonly ILocalizationService _localizationService;

        public SettingsMenuModel(IAudioVolumeControllerService volumeController, ISaveLoadService saveLoadService,
            AudioVolumeSettings volumeSettings, ILocalizationService localizationService)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(SettingsMenuModel), volumeController, saveLoadService, volumeSettings);

            _volumeController = volumeController;
            _saveLoadService = saveLoadService;
            _volumeSettings = volumeSettings;
            _localizationService = localizationService;

            SetLoadedVolumeSettings(volumeSettings);
        }

        public void SetRussianLanguage()
        {
            _localizationService.SetLanguage("ru");
        }

        public void SetEnglishLanguage()
        {
            _localizationService.SetLanguage("en");
        }

        public void SetTurkishLanguage()
        {
            _localizationService.SetLanguage("tr");
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

        private void SetLoadedVolumeSettings(AudioVolumeSettings volumeSettings)
        {
            _volumeController.ToggleMute(volumeSettings.Muted);
            _volumeController.SetMasterVolume(_volumeSettings.MasterVolume);
            _volumeController.SetMusicVolume(_volumeSettings.MusicVolume);
            _volumeController.SetSoundsVolume(_volumeSettings.SoundsVolume);
        }
    }
}
