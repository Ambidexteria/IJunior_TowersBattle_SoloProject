namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarPresenter
    {
        private CannonEnergyBar _model;
        private CannonEnergyBarView _view;

        public CannonEnergyBarPresenter(CannonEnergyBar model, CannonEnergyBarView cannonEnergyBarView)
        {
            _model = model;
            _view = cannonEnergyBarView;
            _view.Init(_model.MaxEnergy);
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
