using System;
using System.Collections;
using UnityEngine;

public class HealthSmoothSliderView : HealthView
{
    [SerializeField] private SliderValueChanger _healthBar;
    [SerializeField] private float _changeSpeed = 0.2f;

    private Coroutine _valueChanger;

    private void Awake()
    {
        if (_healthBar == null)
            throw new ArgumentNullException();
    }

    public override void Display(float value)
    {
        if (_valueChanger != null)
        {
            StopCoroutine(_valueChanger);
        }

        float valuePart = value / GetMaxHealth();
        _valueChanger = StartCoroutine(ChangeValueCoroutine(valuePart));
    }

    private IEnumerator ChangeValueCoroutine(float targetValuePart)
    {
        float value = _healthBar.Value;

        while (value != targetValuePart)
        {
            value = Mathf.MoveTowards(value, targetValuePart, _changeSpeed * Time.deltaTime);
            _healthBar.SetValue(value);

            yield return null;
        }
    }
}