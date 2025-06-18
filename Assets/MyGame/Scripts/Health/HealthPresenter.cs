namespace Base.Health
{
    public class HealthPresenter
    {
        private readonly HealthModel _model;
        private readonly HealthView _view;

        public HealthPresenter(HealthModel model, HealthView view)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(HealthPresenter), model, view);

            _model = model;
            _view = view;
            _view.SetMaxHealth(_model.MaxValue);
        }

        public void Enable()
        {
            _model.Changed += OnHealthChanged;
        }

        public void Disable()
        {
            _model.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(float amount)
        {
            _view.Display(amount);
        }
    }
}
