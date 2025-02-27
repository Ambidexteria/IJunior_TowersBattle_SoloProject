using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValueChanger : MonoBehaviour
{
    private Slider _slider;

    public Action<float> ValueChanged;

    public float Value => _slider.value;
    public float MinValue => _slider.minValue;
    public float MaxValue => _slider.maxValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        if (_slider == null)
            throw new ArgumentNullException();
    }

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

    private void OnValueChanged(float value)
    {
        ValueChanged?.Invoke(value);
    }
}