namespace Base.GameLogic.Cannon
{
    public class CannonEnergyRatePresenter
    {
        private CannonEnergyRateView _view;
        private CannonEnergyRateModel _model;

        public CannonEnergyRatePresenter(CannonEnergyRateView view, CannonEnergyRateModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _model.EnergyIncomeChanged += OnEnergyIncomeChanged;
        }

        public void Disable()
        {
            _model.EnergyIncomeChanged -= OnEnergyIncomeChanged;
        }

        private void OnEnergyIncomeChanged(int energyIncome)
        {
            _view.SetValue(energyIncome);
        }
    }
}
