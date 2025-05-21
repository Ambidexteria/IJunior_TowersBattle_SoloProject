
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
            _view.PauseButtonClicked += Pause;
            _view.ResumeButtonClicked += Resume;
            _view.RestartLevelButtonClicked += OnRestartLevelButtonClicked;
            _view.ReturnToMainMenuButtonClicked += OnReturnToMainMenuButtonClicked;
        }

        public void Disable()
        {
            _view.PauseButtonClicked -= Pause;
            _view.ResumeButtonClicked -= Resume;
            _view.RestartLevelButtonClicked -= OnRestartLevelButtonClicked;
            _view.ReturnToMainMenuButtonClicked -= OnReturnToMainMenuButtonClicked;
        }

        private void OnRestartLevelButtonClicked()
        {
            _model.RestartLevel();
        }

        private void OnReturnToMainMenuButtonClicked()
        {
            _model.ReturnToMainMenu();
        }

        private void Pause()
        {
            Debug.LogWarning("Pause Presenter");
            _model.Pause();
        }

        private void Resume()
        {
            _model.Resume();
        }
    }
}
