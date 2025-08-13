using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValueChanger : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public Action<float> ValueChanged;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }

    public void SetMinMaxValues(float min, float max)
    {
        _slider.minValue = min;
        _slider.maxValue = max;
    }

    private void OnValueChanged(float value)
    {
        ValueChanged?.Invoke(value);
    }
}