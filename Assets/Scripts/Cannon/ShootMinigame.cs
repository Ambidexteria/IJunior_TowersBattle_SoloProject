using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class ShootMinigame : MonoBehaviour
{
    [SerializeField] private MinigameLaunchButtonController _launchButtonController;
    [SerializeField] private UIWindowController _minigameUI;
    [SerializeField] private MinigamePressRange _minigamePressRange;
    [SerializeField] private SliderValueChanger _slider;
    [SerializeField] private ButtonClickHandler _shootButton;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private float _sliderSpeedRate;

    private PlayerInput _playerInput;
    private Coroutine _coroutine;
    private bool _activated = false;

    private float _defaultTimeScale;
    private float _minPressValue;
    private float _maxPressValue;
    private float _sliderSpeed;

    public bool Activated => _activated;

    public event Action Winned;
    public event Action Loosed;

    private void OnEnable()
    {
        _shootButton.Clicked += OnShootButtonPressed;
        _launchButtonController.Clicked += OnLaunchButtonPressed;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _shootButton.Clicked -= OnShootButtonPressed;
        _launchButtonController.Clicked -= OnLaunchButtonPressed;
    }

    [Inject]
    private void Init(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    public void Activate()
    {
        _launchButtonController.Enable();
        _activated = true;
    }

    private void OnLaunchButtonPressed()
    {
        _minigameUI.Show();

        _minigamePressRange.Place();
        _slider.SetMinMaxValues(_minigamePressRange.FullRangeMinValue, _minigamePressRange.FullRangeMaxValue);
        _sliderSpeed = GetSliderSpeed();
        _coroutine = StartCoroutine(MoveSliderCoroutine(_sliderSpeed));

        _launchButtonController.Disable();
    }

    private IEnumerator MoveSliderCoroutine(float speed)
    {
        _timeController.SetSlowMotionTimeScale();
        speed /= _timeController.SlowMotionTimeScale;

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
        _timeController.SetDefaultTimeScale();

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

        _minigameUI.Hide();
        _activated = false;
    }

    private float GetSliderSpeed()
    {
        return (_slider.MaxValue - _slider.MinValue) / _sliderSpeedRate;
    }
}
