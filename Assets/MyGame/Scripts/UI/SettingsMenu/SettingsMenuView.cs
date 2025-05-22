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
    }
}
