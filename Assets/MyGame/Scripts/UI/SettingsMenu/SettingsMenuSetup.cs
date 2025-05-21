using Base.Services.Audio;
using UnityEngine;

namespace Base.UI.Settings
{
    public class SettingsMenuSetup : MonoBehaviour
    {
        [SerializeField] private SettingsMenuView _view;

        private SettingsMenuModel _model;
        private SettingsMenuPresenter _presenter;

        private void OnEnable()
        {
            _presenter?.Enable();
        }

        private void OnDisable()
        {
            _presenter?.Disable();
        }

        public SettingsMenuModel CreateModel(AudioVolumeControllerService audioService)
        {
            _model = new SettingsMenuModel(audioService);
            _presenter = new SettingsMenuPresenter(_view, _model);
            _presenter.Enable();

            return _model;
        }
    }

}
