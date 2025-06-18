using Base.Data;
using System;
using UnityEngine;

namespace Base.UI.Settings
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private SliderValueChanger _masterVolumeSlider;
        [SerializeField] private SliderValueChanger _soundsVolumeSlider;
        [SerializeField] private SliderValueChanger _musicVolumeSlider;
        [SerializeField] private ToggleValueChanger _muteToggle;

        public event Action<float> MasterVolumeChanged;
        public event Action<float> SoundsVolumeChanged;
        public event Action<float> MusicVolumeChanged;
        public event Action<bool> MuteValueChanged;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(SettingsMenuView), nameof(Awake), _masterVolumeSlider, _soundsVolumeSlider, 
                _musicVolumeSlider, _muteToggle);
        }

        public void Enable()
        {
            _masterVolumeSlider.ValueChanged += MasterVolumeChanged;
            _soundsVolumeSlider.ValueChanged += SoundsVolumeChanged;
            _musicVolumeSlider.ValueChanged += MusicVolumeChanged;
            _muteToggle.ValueChanged += MuteValueChanged;
        }

        public void Disable()
        {
            _masterVolumeSlider.ValueChanged -= MasterVolumeChanged;
            _soundsVolumeSlider.ValueChanged -= SoundsVolumeChanged;
            _musicVolumeSlider.ValueChanged -= MusicVolumeChanged;
            _muteToggle.ValueChanged -= MuteValueChanged;
        }

        public void Init(AudioVolumeSettings volumeSettings)
        {
            ExceptionsTest.NullRefMethodTest(nameof(SettingsMenuView), nameof(Init), volumeSettings);

            _masterVolumeSlider.SetValue(volumeSettings.MasterVolume);
            _musicVolumeSlider.SetValue(volumeSettings.MusicVolume);
            _soundsVolumeSlider.SetValue(volumeSettings.SoundsVolume);

            _muteToggle.SetValue(volumeSettings.Muted);
        }
    }
}
