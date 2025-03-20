using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class ShootMinigame : MonoBehaviour
{
    [SerializeField] private MinigamePressRange _minigamePressRange;
    [SerializeField] private SliderValueChanger _slider;
    [SerializeField] private ButtonClickHandler _shootButton;
    [SerializeField] private float _sliderSpeedRate;

    private PlayerInput _playerInput;
    private Coroutine _coroutine;

    private float _minPressValue;
    private float _maxPressValue;

    public event Action Winned;
    public event Action Loosed;

    private void OnEnable()
    {
        _shootButton.Clicked += OnShootButtonPressed;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _shootButton.Clicked -= OnShootButtonPressed;
    }

    [Inject]
    private void Init(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    public void Launch()
    {
        _minigamePressRange.Place();
        _slider.SetMinMaxValues(_minigamePressRange.FullRangeMinValue, _minigamePressRange.FullRangeMaxValue);

        float sliderSpeed = GetSliderSpeed();
        _coroutine = StartCoroutine(MoveSliderCoroutine(sliderSpeed));
    }

    private IEnumerator MoveSliderCoroutine(float speed)
    {
        float nextValue;
        _slider.SetValue(_slider.MinValue);

        while (true)
        {
            nextValue = _slider.Value + speed * Time.deltaTime;
            nextValue = Mathf.Clamp(nextValue, _slider.MinValue, _slider.MaxValue);

            _slider.SetValue(nextValue);

            if (nextValue == _slider.MaxValue || nextValue == _slider.MinValue)
            {
                speed *= -1;
            }

            yield return null;
        }
    }

    private void OnShootButtonPressed()
    {
        StopCoroutine(_coroutine);

        float value = _slider.Value;

        if (value >= _minigamePressRange.MinPressValue && value <= _minigamePressRange.MaxPressValue)
        {
            Winned?.Invoke();
        }
        else
        {
            Loosed?.Invoke();
        }
    }

    private float GetSliderSpeed()
    {
        return (_slider.MaxValue - _slider.MinValue) / _sliderSpeedRate;
    }
}
