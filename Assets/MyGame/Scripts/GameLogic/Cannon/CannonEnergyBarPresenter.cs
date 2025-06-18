namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarPresenter
    {
        private CannonEnergyBarModel _model;
        private CannonEnergyBarView _view;

        public CannonEnergyBarPresenter(CannonEnergyBarModel model, CannonEnergyBarView cannonEnergyBarView)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonEnergyBarPresenter), ExceptionsTest.ConstructorName, model, cannonEnergyBarView);

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
