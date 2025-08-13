using Base.Data;
using Base.Data.Game;
using System;
using UnityEngine;

namespace Base.UI.Settings
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private ToggleValueChanger _tutorialToggle;
        [SerializeField] private ButtonClickHandler _russianLanguageButton;
        [SerializeField] private ButtonClickHandler _englishLanguageButton;
        [SerializeField] private ButtonClickHandler _turkishLanguageButton;
        [SerializeField] private SliderValueChanger _masterVolumeSlider;
        [SerializeField] private SliderValueChanger _soundsVolumeSlider;
        [SerializeField] private SliderValueChanger _musicVolumeSlider;
        [SerializeField] private ToggleValueChanger _muteToggle;

        public event Action RussianLanguageButtonCLicked;
        public event Action EnglishLanguageButtonCLicked;
        public event Action TurkishLanguageButtonCLicked;
        public event Action<float> MasterVolumeChanged;
        public event Action<float> SoundsVolumeChanged;
        public event Action<float> MusicVolumeChanged;
        public event Action<bool> MuteValueChanged;
        public event Action<bool> TutorialValueChanged;

        public void Enable()
        {
            _russianLanguageButton.Clicked += RussianLanguageButtonCLicked;
            _englishLanguageButton.Clicked += EnglishLanguageButtonCLicked;
            _turkishLanguageButton.Clicked += TurkishLanguageButtonCLicked;
            _masterVolumeSlider.ValueChanged += MasterVolumeChanged;
            _soundsVolumeSlider.ValueChanged += SoundsVolumeChanged;
            _musicVolumeSlider.ValueChanged += MusicVolumeChanged;
            _muteToggle.ValueChanged += MuteValueChanged;
            _tutorialToggle.ValueChanged += TutorialValueChanged;
        }

        public void Disable()
        {
            _russianLanguageButton.Clicked -= RussianLanguageButtonCLicked;
            _englishLanguageButton.Clicked -= EnglishLanguageButtonCLicked;
            _turkishLanguageButton.Clicked -= TurkishLanguageButtonCLicked;
            _masterVolumeSlider.ValueChanged -= MasterVolumeChanged;
            _soundsVolumeSlider.ValueChanged -= SoundsVolumeChanged;
            _musicVolumeSlider.ValueChanged -= MusicVolumeChanged;
            _muteToggle.ValueChanged -= MuteValueChanged;
            _tutorialToggle.ValueChanged -= TutorialValueChanged;
        }

        public void Init(GameSettings gameSettings)
        {
            AudioVolumeSettings volumeSettings = gameSettings.AudioVolumeSettings;

            _masterVolumeSlider.SetValue(volumeSettings.MasterVolume);
            _musicVolumeSlider.SetValue(volumeSettings.MusicVolume);
            _soundsVolumeSlider.SetValue(volumeSettings.SoundsVolume);
            _muteToggle.SetValue(volumeSettings.Muted);

            _tutorialToggle.SetValue(gameSettings.TutorialEnabled);
        }
    }
}
