using Base.GameLogic.Cannon;
using UnityEngine;

public class CannonEnergyRateSetup : MonoBehaviour
{
    [SerializeField] private CannonEnergyRateView _view;

    private CannonEnergyRateModel _model;
    private CannonEnergyRatePresenter _presenter;

    public CannonEnergyRateModel Create(CannonEnergyBarModel energyBar)
    {
        _model = new(energyBar);
        _model.Enable();

        _presenter = new(_view, _model);
        _presenter.Enable();

        return _model;
    }
}
