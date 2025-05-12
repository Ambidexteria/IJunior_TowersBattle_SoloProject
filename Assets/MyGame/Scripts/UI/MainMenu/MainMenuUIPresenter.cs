using System;

namespace Base.UI.MainMenu
{
    public class MainMenuUIPresenter
    {
        private MainMenuUIView _view;
        private MainMenuUIModel _model;

        public event Action StartBattleButtonClicked;

        public MainMenuUIPresenter(MainMenuUIView view, MainMenuUIModel presenter)
        {
            _view = view;
            _model = presenter;
        }

        public void Enable()
        {
            _view.Show();
            _view.StartButtonClicked += OnStartButtonClicked;
            _model.Enabled += OnModelEnabled;
            _model.Disabled += OmModelDisabled;
        }

        private void OmModelDisabled()
        {
            _view.Hide();
        }

        private void OnModelEnabled()
        {
            _view.Show();
        }

        private void OnStartButtonClicked()
        {
            //_view.Hide();
            _model.StartBattle();
        }
    }
}
