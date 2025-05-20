using System;
using UnityEngine;

namespace Base.UI.PauseMenu
{
    public class PauseMenuPresenter
    {
        private readonly PauseMenuView _view;
        private readonly PauseMenuModel _model;

        public PauseMenuPresenter(PauseMenuView view, PauseMenuModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.ResumeButtonClicked += OnResumeButtonClicked;
            _view.RestartLevelButtonClicked += OnRestartLevelButtonClicked;
            _view.ReturnToMainMenuButtonClicked += OnReturnToMainMenuButtonClicked;
            _view.SettingsButtonClicked += OnSettingsButtonClicked;
        }

        public void Disable()
        {
            _view.ResumeButtonClicked -= OnResumeButtonClicked;
            _view.RestartLevelButtonClicked -= OnRestartLevelButtonClicked;
            _view.ReturnToMainMenuButtonClicked -= OnReturnToMainMenuButtonClicked;
            _view.SettingsButtonClicked -= OnSettingsButtonClicked;
        }

        private void OnResumeButtonClicked()
        {
            _model.Resume();
        }

        private void OnRestartLevelButtonClicked()
        {
            _model.RestartLevel();
        }

        private void OnReturnToMainMenuButtonClicked()
        {
            _model.ReturnToMainMenu();
        }

        private void OnSettingsButtonClicked()
        {
            _model.ShowSettingsMenu();
        }
    }
}
