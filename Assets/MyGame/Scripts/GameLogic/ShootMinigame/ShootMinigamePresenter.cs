using System;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigamePresenter
    {
        private readonly ShootMinigameModel _model;
        private readonly ShootMinigameView _view;

        public ShootMinigamePresenter(ShootMinigameModel model, ShootMinigameView view)
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShootMinigamePresenter), ExceptionsTest.ConstructorName, model, view);

            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _view.LaunchButtonClicked += OnLaunchButtonClicked;
            _view.ShootButtonClicked += OnShootButtonClicked;

            _model.ReadyForShoot += OnReadyForShoot;
        }

        public void Disable()
        {
            _view.LaunchButtonClicked -= OnLaunchButtonClicked;
            _view.ShootButtonClicked -= OnShootButtonClicked;

            _model.ReadyForShoot -= OnReadyForShoot;
        }

        private void OnReadyForShoot()
        {
            _view.EnableLaunchButton();
        }

        private void OnLaunchButtonClicked()
        {
            _model.LaunchMinigame();
            //_view.ShowPressRange();
        }

        private void OnShootButtonClicked()
        {
            _model.EndMinigame();
            //_view.HidePressRange();
        }
    }
}
