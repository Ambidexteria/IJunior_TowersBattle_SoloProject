namespace Base.GameLogic.Cannon
{
    public class CannonHealthPresenter
    {
        private readonly CannonModel _model;
        private readonly CannonHealthView _healthView;

        public CannonHealthPresenter(CannonModel model, CannonHealthView healthView)
        {
            _model = model;
            _healthView = healthView;
            _healthView.SetMaxHealth(_model.MaxHealth);

            _model.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float amount)
        {
            _healthView.Display(amount);
        }
    }
}
