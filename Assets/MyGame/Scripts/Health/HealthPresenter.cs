namespace Base.Health
{
    public class HealthPresenter
    {
        private readonly HealthModel _model;
        private readonly HealthView _healthView;

        public HealthPresenter(HealthModel model, HealthView healthView)
        {
            _model = model;
            _healthView = healthView;
            _healthView.SetMaxHealth(_model.MaxValue);
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
            _healthView.Display(amount);
        }
    }
}
