using Base.Services.SceneManagment;

namespace Base.UI.PauseMenu
{
    public class PauseMenuPresenter
    {
        private readonly PauseMenuView _view;
        private readonly PauseMenuModel _model;

        public PauseMenuPresenter(PauseMenuView view, PauseMenuModel model)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(PauseMenuModel), view, model);

            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.RestartLevelButtonClicked += OnRestartLevelButtonClicked;
            _view.ReturnToMainMenuButtonClicked += OnReturnToMainMenuButtonClicked;
        }

        public void Disable()
        {
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
    }
}
