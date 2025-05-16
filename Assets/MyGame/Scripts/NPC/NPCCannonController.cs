using Base.GameLogic.Cannon;
using System;

public class NPCCannonController
{
    private CannonModel _cannon;
    private CannonEnergyBar _energyBar;

    private bool _active = true;

    public NPCCannonController(CannonModel cannon, CannonEnergyBar energyBar)
    {
        _cannon = cannon;
        _energyBar = energyBar;
    }

    public event Action CannonDestroyed;

    public void Enable()
    {
        _energyBar.Enable();

        _energyBar.Filled += OnEnergyBarFilled;
        _cannon.Destroyed += OnCannonDestroyed;
    }

    public void Disable()
    {
        Stop();

        _energyBar.Filled -= OnEnergyBarFilled;
        _cannon.Destroyed -= OnCannonDestroyed;
    }

    private void Stop()
    {
        _active = false;
        _energyBar.Disable();
    }

    private void OnEnergyBarFilled()
    {
        if (_active)
        {
            _cannon.Shoot();
            _energyBar.RemoveCurrentEnergy();
        }
    }

    private void OnCannonDestroyed()
    {
        _active = false;
        CannonDestroyed?.Invoke();
    }
}
