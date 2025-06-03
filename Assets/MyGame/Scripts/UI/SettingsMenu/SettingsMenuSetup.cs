using Base.Data;
using Base.Services.Audio;
using Base.Services.SaveLoad;
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

        public SettingsMenuModel CreateModel(IAudioVolumeControllerService audioService, ISaveLoadService saveLoadService,
            AudioVolumeSettings volumeSettings)
        {
            _model = new SettingsMenuModel(audioService, saveLoadService, volumeSettings);
            _presenter = new SettingsMenuPresenter(_view, _model);

            _presenter.Enable();

            _view.Init(volumeSettings);
            _view.Enable();

            return _model;
        }
    }

}
