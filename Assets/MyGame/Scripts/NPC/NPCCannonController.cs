using Base.GameLogic.Cannon;
using System;
using UnityEngine;

public class NPCCannonController : MonoBehaviour
{
    [SerializeField] private CannonModel _cannon;
    [SerializeField] private CannonEnergyBar _energyBar;

    private bool _active = true;

    public event Action CannonDestroyed;

    private void OnEnable()
    {
        _energyBar.Filled += OnEnergyBarFilled;
        _cannon.Destroyed += OnCannonDestroyed;
    }

    private void OnDisable()
    {
        _energyBar.Filled -= OnEnergyBarFilled;
        _cannon.Destroyed -= OnCannonDestroyed;
    }

    public void Stop()
    {
        _active = false;
        _energyBar.Stop();
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
