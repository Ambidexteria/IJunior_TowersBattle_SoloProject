namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarPresenter
    {
        private readonly CannonEnergyBarModel _model;
        private readonly CannonEnergyBarView _view;

        public CannonEnergyBarPresenter(CannonEnergyBarModel model, CannonEnergyBarView cannonEnergyBarView)
        {
            _model = model;
            _view = cannonEnergyBarView;
            _view.SetMaxEnergy(_model.MaxEnergy);
        }

        public void Enable()
        {
            _model.CurrentEnergyChanged += OnCurrentEnergyChanged;
        }

        public void Disable()
        {
            _model.CurrentEnergyChanged -= OnCurrentEnergyChanged;
        }

        private void OnCurrentEnergyChanged(float amount)
        {
            _view.Display(amount);
        }
    }
}
