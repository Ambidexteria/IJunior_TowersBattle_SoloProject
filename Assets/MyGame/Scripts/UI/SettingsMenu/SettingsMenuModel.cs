using Base.Data;
using Base.Data.Game;
using Base.Services.Audio;
using Base.Services.Localization;
using Base.Services.SaveLoad;

namespace Base.UI.Settings
{
    public class SettingsMenuModel
    {
        private const string LanguageCodeRU = "ru";
        private const string LanguageCodeEN = "en";
        private const string LanguageCodeTR = "tr";

        private readonly IAudioVolumeControllerService _volumeController;
        private readonly ISaveLoadService _saveLoadService;
        private readonly GameSettings _gameSettings;
        private readonly AudioVolumeSettings _volumeSettings;
        private readonly ILocalizationService _localizationService;

        public SettingsMenuModel(IAudioVolumeControllerService volumeController, ISaveLoadService saveLoadService,
            GameSettings gameSettings, ILocalizationService localizationService)
        {
            _volumeController = volumeController;
            _saveLoadService = saveLoadService;
            _gameSettings = gameSettings;
            _volumeSettings = _gameSettings.AudioVolumeSettings;
            _localizationService = localizationService;

            SetLoadedVolumeSettings();
        }

        public void SetRussianLanguage()
        {
            _localizationService.SetLanguage(LanguageCodeRU);
        }

        public void SetEnglishLanguage()
        {
            _localizationService.SetLanguage(LanguageCodeEN);
        }

        public void SetTurkishLanguage()
        {
            _localizationService.SetLanguage(LanguageCodeTR);
        }

        public void ToggleMute(bool value)
        {
            _volumeController.ToggleMute(value);
            _volumeSettings.Muted = value;
            SaveSettings();
        }

        public void ToggleTutorial(bool value)
        {
            _gameSettings.TutorialEnabled = value;
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

        private void SetLoadedVolumeSettings()
        {
            _volumeController.ToggleMute(_volumeSettings.Muted);
            _volumeController.SetMasterVolume(_volumeSettings.MasterVolume);
            _volumeController.SetMusicVolume(_volumeSettings.MusicVolume);
            _volumeController.SetSoundsVolume(_volumeSettings.SoundsVolume);
        }
    }
}
