using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class ShootMinigame : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _pressRange = 0.1f;
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

    [ContextMenu(nameof(Launch))]
    private void Launch()
    {
        float sliderSpeed = GetSliderSpeed();
        _coroutine = StartCoroutine(MoveSliderCoroutine(sliderSpeed));
    }

    private IEnumerator MoveSliderCoroutine(float speed)
    {
        float nextValue;
        float currentSpeed = _sliderSpeedRate;
        _slider.SetValue(_slider.MinValue);

        while (true)
        {
            nextValue = _slider.Value + currentSpeed * Time.deltaTime;
            nextValue = Mathf.Clamp(nextValue, _slider.MinValue, _slider.MaxValue);

            _slider.SetValue(nextValue);

            if (nextValue == _slider.MaxValue || nextValue == _slider.MinValue)
            {
                currentSpeed *= -1;
            }

            yield return null;
        }
    }

    private void OnShootButtonPressed()
    {
        CalculatePressRangeValues();
        StopCoroutine(_coroutine);

        float value = _slider.Value;

        if (value >= _minPressValue && value <= _maxPressValue)
        {
            Winned?.Invoke();
            Debug.Log("Winned!");
        }
        else
        {
            Loosed?.Invoke();
            Debug.Log("Loosed!");
        }
    }

    private float GetSliderSpeed()
    {
        return (_slider.MaxValue - _slider.MinValue) / _sliderSpeedRate;
    }

    private void CalculatePressRangeValues()
    {
        float fullRange = (_slider.MaxValue - _slider.MinValue);

        _minPressValue = UnityEngine.Random.Range(0, fullRange);
        _maxPressValue = _minPressValue + fullRange * _pressRange;
    }
}
