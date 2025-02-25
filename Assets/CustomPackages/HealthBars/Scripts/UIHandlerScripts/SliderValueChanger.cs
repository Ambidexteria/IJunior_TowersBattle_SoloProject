using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValueChanger : MonoBehaviour
{
    private Slider _slider;

    public Action<float> ValueChanged;

    public float Value => _slider.value;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        if (_slider == null)
            throw new ArgumentNullException();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeValue);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeValue);
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }

    private void ChangeValue(float value)
    {
        ValueChanged?.Invoke(value);
    }
}