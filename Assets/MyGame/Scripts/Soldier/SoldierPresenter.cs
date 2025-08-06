namespace Base.Soldier
{
    public class SoldierPresenter
    {
        private readonly SoldierModel _model;
        private readonly SoldierView _view;

        public SoldierPresenter(SoldierModel model, SoldierView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.HealthChanged += OnHealthChanged;
        }

        public void Disable()
        {
            _model.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float health)
        {
            _view.DisplayHealth(health);
        }
    }
}
