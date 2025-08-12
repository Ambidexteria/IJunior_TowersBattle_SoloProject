using System;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyRateModel
    {
        private readonly CannonEnergyBarModel _barModel;

        public CannonEnergyRateModel(CannonEnergyBarModel barModel)
        {
            _barModel = barModel;
        }

        public event Action<int> EnergyIncomeChanged;

        public void Enable()
        {
            _barModel.EnergyIncomeChanged += OnEnergyIncomeChanged;
        }

        public void DIsable()
        {
            _barModel.EnergyIncomeChanged -= OnEnergyIncomeChanged;
        }

        private void OnEnergyIncomeChanged(int energyIncome)
        {
            EnergyIncomeChanged?.Invoke(energyIncome);
        }
    }
}
