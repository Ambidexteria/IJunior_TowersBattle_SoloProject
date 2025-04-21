using Base.Cannon;
using System;
using UnityEngine;

public abstract class CannonHealthView : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;

    private void Awake()
    {
        if (_cannon == null)
            throw new ArgumentNullException();

        PrepareOnAwake();
    }

    private void Start()
    {
        Display(_cannon.CurrentHealth);        
    }

    private void OnEnable()
    {
        _cannon.HealthChanged += Display;
    }

    private void OnDisable()
    {
        _cannon.HealthChanged -= Display;
    }

    public float GetMaxHealth()
    {
        return _cannon.MaxHealth;
    }

    public abstract void Display(float value);

    public abstract void PrepareOnAwake();
}