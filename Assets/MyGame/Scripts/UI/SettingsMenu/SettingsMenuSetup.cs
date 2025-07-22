using Base.Data;
using Base.Data.Game;
using Base.Services.Audio;
using Base.Services.Localization;
using Base.Services.SaveLoad;
using UnityEngine;

namespace Base.UI.Settings
{
    public class SettingsMenuSetup : MonoBehaviour
    {
        [SerializeField] private SettingsMenuView _view;

        private SettingsMenuModel _model;
        private SettingsMenuPresenter _presenter;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(SettingsMenuSetup), nameof(Awake), _view);
        }

        private void OnEnable()
        {
            _presenter?.Enable();
        }

        private void OnDisable()
        {
            _presenter?.Disable();
        }

        public SettingsMenuModel CreateModel(IAudioVolumeControllerService audioService, ISaveLoadService saveLoadService,
            AudioVolumeSettings volumeSettings, GameSettings gameSettings, ILocalizationService localizationService)
        {
            ExceptionsTest.NullRefMethodTest(nameof(SettingsMenuSetup), nameof(CreateModel), audioService, saveLoadService, volumeSettings,
                localizationService);

            _model = new SettingsMenuModel(audioService, saveLoadService, volumeSettings, gameSettings, localizationService);
            _presenter = new SettingsMenuPresenter(_view, _model);

            _presenter.Enable();

            _view.Init(volumeSettings, gameSettings);
            _view.Enable();

            return _model;
        }
    }

}
