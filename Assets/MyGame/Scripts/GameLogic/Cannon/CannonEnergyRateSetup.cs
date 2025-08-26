using Base.GameLogic.Cannon;
using UnityEngine;

public class CannonEnergyRateSetup : MonoBehaviour
{
    [SerializeField] private CannonEnergyRateView _view;

    private CannonEnergyRateModel _model;
    private CannonEnergyRatePresenter _presenter;

    public CannonEnergyRateModel Create(CannonEnergyBarModel energyBar)
    {
        _model = new CannonEnergyRateModel(energyBar);
        _model.Enable();

        _presenter = new CannonEnergyRatePresenter(_view, _model);
        _presenter.Enable();

        return _model;
    }
}
