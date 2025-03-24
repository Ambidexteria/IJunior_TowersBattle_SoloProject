using System;
using UnityEngine;

public class NPCCannonController : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private CannonEnergyBar _energyBar;

    private bool _isCannonAlive = true;

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

    private void OnEnergyBarFilled()
    {
        if (_isCannonAlive)
            _cannon.Shoot();
    }

    private void OnCannonDestroyed()
    {
        _isCannonAlive = false;
        CannonDestroyed?.Invoke();
    }
}
