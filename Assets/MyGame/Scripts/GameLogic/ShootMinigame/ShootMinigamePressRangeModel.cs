using Base.Infrastructure;
using Base.Services.TimeManagment;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigamePressRangeModel
    {
        private readonly TimeController _timeController;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _pressRangeWidthCoefficient = 0.1f;
        private readonly float _sliderSpeedRate;
        private RectTransform _fullRangeRectTransform;

        private float _sliderSpeed;
        private float _minPressValue;
        private float _maxPressValue;
        private float _pressRangeWidth;
        private float _currentValue;
        private Coroutine _coroutine;

        private bool _enabled = false;

        public ShootMinigamePressRangeModel(float pressRangeWidthCoefficienr, float sliderSpeedRate, RectTransform fullPressRange,
            TimeController timeController, ICoroutineRunner coroutineRunner)
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShootMinigamePressRangeModel), ExceptionsTest.ConstructorName, fullPressRange, 
                timeController, coroutineRunner);

            _pressRangeWidthCoefficient = pressRangeWidthCoefficienr;
            _sliderSpeedRate = sliderSpeedRate;
            _fullRangeRectTransform = fullPressRange;
            _timeController = timeController;
            _coroutineRunner = coroutineRunner;

            CalculateStaticValues();
        }

        public float PressRangeWidth => _pressRangeWidth;
        public float FullRangeMinValue => _fullRangeRectTransform.anchoredPosition.x;
        public float FullRangeMaxValue => _fullRangeRectTransform.rect.width;

        public event Action<float> ValueChanged;
        public event Action<float> PlacingPressRange;

        public void Enable()
        {
            if (_enabled) 
                return; 

            PlaceRange();
            _enabled = true;
        }

        public void Disable()
        {
            if (_enabled == false)
                return;

            if (_coroutine != null)
                _coroutineRunner.EndCoroutine(_coroutine);

            _enabled = false;
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

            _currentValue = FullRangeMinValue;
            float nextValue;
            ValueChanged?.Invoke(FullRangeMinValue);

            while (true)
            {
                nextValue = _currentValue + speed * Time.deltaTime;
                _currentValue = nextValue;
                nextValue = Mathf.Clamp(nextValue, FullRangeMinValue, FullRangeMaxValue);

                ValueChanged?.Invoke(nextValue);

                if (nextValue == FullRangeMaxValue || nextValue == FullRangeMinValue)
                {
                    speed *= -1;
                }

                yield return null;
            }
        }

        private void CalculateStaticValues()
        {
            _pressRangeWidth = FullRangeMaxValue * _pressRangeWidthCoefficient;
            _sliderSpeed = (FullRangeMaxValue - FullRangeMinValue) / _sliderSpeedRate;
        }

        private void CalculatePressRangeValues()
        {
            _minPressValue = UnityEngine.Random.Range(0, FullRangeMaxValue - _pressRangeWidth);
            _maxPressValue = _minPressValue + _pressRangeWidth;
        }
    }
}
