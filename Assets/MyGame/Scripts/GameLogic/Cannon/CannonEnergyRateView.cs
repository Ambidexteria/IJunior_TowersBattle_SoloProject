using System;
using TMPro;
using UnityEngine;

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

    public class CannonEnergyRateModel
    {
        private CannonEnergyBarModel _barModel;

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

    public class CannonEnergyRateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _energyRate;

        public void SetValue(int energyRate)
        {
            _energyRate.text = energyRate.ToString();
        }
    }
}
