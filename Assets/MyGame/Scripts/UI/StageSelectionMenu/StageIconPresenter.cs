namespace Base.UI.StageSelection
{
    public class StageIconPresenter
    {
        private readonly StageIconView _view;
        private readonly StageIconModel _model;

        public StageIconPresenter(StageIconView view, StageIconModel model)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(StageIconPresenter), view, model);

            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.Clicked += OnViewCLicked;

            _model.BorderEnabled += OnBorderEnabled;
            _model.BorderDisabled += OnBorderDisabled;
        }

        public void Disable()
        {
            _view.Clicked -= OnViewCLicked;

            _model.BorderEnabled -= OnBorderEnabled;
            _model.BorderDisabled -= OnBorderDisabled;
        }

        private void OnBorderEnabled()
        {
            _view.ShowBorder();
        }

        private void OnBorderDisabled()
        {
            _view.HideBorder();
        }

        private void OnViewCLicked(StageIconView view)
        {
            ExceptionsTest.NullRefMethodTest(nameof(StageIconPresenter), nameof(OnViewCLicked), view);

            _model.Choose();
        }
    }
}
