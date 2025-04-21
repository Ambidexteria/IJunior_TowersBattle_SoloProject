using System;
using UnityEngine;

public class Health
{
    private float _maxValue;
    private float _currentHealth;

    public Health(float maxValue)
    {
        _maxValue = maxValue;

        Current = _maxValue;
    }

    public event Action Dying;
    public event Action<float> Changed;

    public float Current
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            if (value <= 0)
                _currentHealth = 0;
            else
                _currentHealth = value;
        }
    }

    public float MaxValue => _maxValue;
    public bool IsDead => Current <= 0;

    public void Increase(float amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount) + " in " + nameof(Health));

        Current += amount;
        Current = Mathf.Clamp(Current, 0, _maxValue);

        Changed?.Invoke(Current);
    }

    public void Decrease(float amount)
    {
        if (IsDead)
            return;

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount) + " in " + nameof(Health));

        Current -= amount;

        Changed?.Invoke(Current);

        if (IsDead)
        {
            Dying?.Invoke();
        }
    }
}