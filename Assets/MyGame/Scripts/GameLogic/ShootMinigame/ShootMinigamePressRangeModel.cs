using System;
using System.Collections;
using Base.Infrastructure;
using Base.Services.TimeManagment;
using UnityEngine;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigamePressRangeModel
    {
        private const float BorderSpacing = 0.1f;

        private readonly TimeController _timeController;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _pressRangeWidthCoefficient;
        private readonly float _sliderSpeedRate;
        private readonly float _fullRangeMinValue;
        private readonly float _fullRangeMaxValue;

        private float _sliderSpeed;
        private float _minPressValue;
        private float _maxPressValue;
        private float _pressRangeWidth;
        private float _currentValue;
        private Coroutine _coroutine;

        private bool _enabled = false;

        public ShootMinigamePressRangeModel(
            float pressRangeWidthCoefficienr,
            float sliderSpeedRate,
            RectTransform fullPressRange,
            TimeController timeController,
            ICoroutineRunner coroutineRunner)
        {
            _pressRangeWidthCoefficient = pressRangeWidthCoefficienr;
            _sliderSpeedRate = sliderSpeedRate;
            _fullRangeMinValue = fullPressRange.anchoredPosition.x;
            _fullRangeMaxValue = fullPressRange.rect.width;
            _timeController = timeController;
            _coroutineRunner = coroutineRunner;

            CalculateStaticValues();
        }

        public event Action<float> ValueChanged;
        public event Action<float> PlacingPressRange;

        public float PressRangeWidth => _pressRangeWidth;

        public void Enable()
        {
            if (_enabled)
                return;

            _enabled = true;

            PlaceRange();
        }

        public void Disable()
        {
            if (_enabled == false)
                return;

            _enabled = false;

            if (_coroutine != null)
                _coroutineRunner.EndCoroutine(_coroutine);
        }

        public bool IsCurrentValueInPressRange()
        {
            return _currentValue >= _minPressValue && _currentValue <= _maxPressValue;
        }

        private void PlaceRange()
        {
            if (_coroutine != null)
                _coroutineRunner.EndCoroutine(_coroutine);

            CalculatePressRangeValues();

            _coroutine = _coroutineRunner.LaunchCoroutine(MoveSliderCoroutine(_sliderSpeed));

            PlacingPressRange?.Invoke(_minPressValue);
        }

        private IEnumerator MoveSliderCoroutine(float speed)
        {
            speed /= _timeController.SlowMotionTimeScale;

            _currentValue = _fullRangeMinValue;
            float nextValue;
            ValueChanged?.Invoke(_fullRangeMinValue);

            while (_enabled)
            {
                nextValue = _currentValue + speed * Time.deltaTime;
                nextValue = Mathf.Clamp(nextValue, _fullRangeMinValue, _fullRangeMaxValue);

                _currentValue = nextValue;

                ValueChanged?.Invoke(nextValue);

                if (IsOutsideFullRange(nextValue))
                {
                    speed *= -1;
                }

                yield return null;
            }
        }

        private bool IsOutsideFullRange(float nextValue)
        {
            return nextValue >= (_fullRangeMaxValue - BorderSpacing) || nextValue <= (_fullRangeMinValue + BorderSpacing);
        }

        private float ChangeSpeedDirection(float currentSpeed, float nextValue)
        {
            float speed = currentSpeed;

            if (nextValue >= (_fullRangeMaxValue - BorderSpacing))
                speed = Mathf.Abs(currentSpeed) * -1;
            else if (nextValue <= (_fullRangeMinValue + BorderSpacing))
                speed = Mathf.Abs(currentSpeed);

            return speed;
        }

        private void CalculateStaticValues()
        {
            _pressRangeWidth = _fullRangeMaxValue * _pressRangeWidthCoefficient;
            _sliderSpeed = (_fullRangeMaxValue - _fullRangeMinValue) / _sliderSpeedRate;
        }

        private void CalculatePressRangeValues()
        {
            _minPressValue = UnityEngine.Random.Range(0, _fullRangeMaxValue - _pressRangeWidth);
            _maxPressValue = _minPressValue + _pressRangeWidth;
        }
    }
}
