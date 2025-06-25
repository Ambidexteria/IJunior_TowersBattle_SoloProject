using System;

namespace Base.UI.Settings
{
    public class SettingsMenuPresenter
    {
        private readonly SettingsMenuView _view;
        private readonly SettingsMenuModel _model;

        public SettingsMenuPresenter(SettingsMenuView view, SettingsMenuModel model)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(SettingsMenuPresenter), view, model);

            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.RussianLanguageButtonCLicked += OnRussianLanguageButtonCLicked;
            _view.EnglishLanguageButtonCLicked += OnEnglishLanguageButtonCLicked;
            _view.TurkishLanguageButtonCLicked += OnTurkishLanguageButtonCLicked;
            _view.MasterVolumeChanged += OnMasterVolumeChanged;
            _view.MusicVolumeChanged += OnMusicVolumeChanged;
            _view.SoundsVolumeChanged += OnSoundsVolumeChanged;
            _view.MuteValueChanged += OnMuteValueChanged;
        }
        public void Disable()
        {
            _view.RussianLanguageButtonCLicked -= OnRussianLanguageButtonCLicked;
            _view.EnglishLanguageButtonCLicked -= OnEnglishLanguageButtonCLicked;
            _view.TurkishLanguageButtonCLicked -= OnTurkishLanguageButtonCLicked;
            _view.MasterVolumeChanged -= OnMasterVolumeChanged;
            _view.MusicVolumeChanged -= OnMusicVolumeChanged;
            _view.SoundsVolumeChanged -= OnSoundsVolumeChanged;
            _view.MuteValueChanged -= OnMuteValueChanged;
        }

        private void OnRussianLanguageButtonCLicked()
        {
            _model.SetRussianLanguage();
        }

        private void OnEnglishLanguageButtonCLicked()
        {
            _model.SetEnglishLanguage();
        }

        private void OnTurkishLanguageButtonCLicked()
        {
            _model.SetTurkishLanguage();
        }

        private void OnMasterVolumeChanged(float value)
        {
            _model.SetMasterVolume(value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            _model.SetMusicVolume(value);
        }

        private void OnSoundsVolumeChanged(float value)
        {
            _model.SetSoundsVolume(value);
        }

        private void OnMuteValueChanged(bool value)
        {
            _model.ToggleMute(value);
        }
    }
}
