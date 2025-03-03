using System;
using UnityEngine;

public class HealthSliderView : HealthView
{
    [SerializeField] private SliderValueChanger _healthBar;

    public override void PrepareOnAwake()
    {
        if (_healthBar == null)
            throw new ArgumentNullException();
    }


    public override void Display(float value)
    {
        float valuePart = value / GetMaxHealth();
        _healthBar.SetValue(valuePart);
    }
}