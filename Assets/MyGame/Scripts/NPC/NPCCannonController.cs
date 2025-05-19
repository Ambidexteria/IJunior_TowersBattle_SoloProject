using Base.GameLogic.Cannon;
using System;

public class NPCCannonController
{
    private CannonModel _cannon;
    private CannonEnergyBar _energyBar;

    private bool _enabled = false;

    public NPCCannonController(CannonModel cannon, CannonEnergyBar energyBar)
    {
        _cannon = cannon;
        _energyBar = energyBar;
    }

    public event Action CannonDestroyed;

    public void Enable()
    {
        if (_enabled)
            return;

        _cannon.Enable();
        _energyBar.Enable();

        _energyBar.Filled += OnEnergyBarFilled;
        _cannon.Destroyed += OnCannonDestroyed;

        _enabled = true;
    }

    public void Disable()
    {
        if(_enabled == false) 
            return;

        _cannon.Disable();
        _energyBar.Disable();

        _energyBar.Filled -= OnEnergyBarFilled;
        _cannon.Destroyed -= OnCannonDestroyed;

        _enabled = false;
    }

    private void OnEnergyBarFilled()
    {
        if (_enabled)
        {
            _cannon.Shoot();
            _energyBar.RemoveCurrentEnergy();
        }
    }

    private void OnCannonDestroyed()
    {
        _enabled = false;
        CannonDestroyed?.Invoke();
    }
}
