using Base.Infrastructure;
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
        private float _pressRangeWidthCoefficient = 0.1f;
        private readonly float _sliderSpeedRate;
        private Image _pressRangeImage;
        private RectTransform _fullRangeRectTransform;

        private float _sliderSpeed;
        private float _minPressValue;
        private float _maxPressValue;
        private float _pressRangeWidth;
        private float _currentValue;
        private Coroutine _coroutine;

        private bool _enabled = false;

        public ShootMinigamePressRangeModel(float pressRangeWidthCoefficienr, float sliderSpeedRate, Image pressRangeImage, RectTransform fullPressRange,
            TimeController timeController, ICoroutineRunner coroutineRunner)
        {
            _pressRangeWidthCoefficient = pressRangeWidthCoefficienr;
            _sliderSpeedRate = sliderSpeedRate;
            _pressRangeImage = pressRangeImage;
            _fullRangeRectTransform = fullPressRange;
            _timeController = timeController;
            _coroutineRunner = coroutineRunner;

            CalculateStaticValues();
        }

        public float PressRangeWidth => _pressRangeWidth;
        public float FullRangeMinValue => _fullRangeRectTransform.anchoredPosition.x;
        public float FullRangeMaxValue => _fullRangeRectTransform.rect.width;

        public event Action<float> ValueChanged;

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
                _coroutineRunner.StopCoroutine(_coroutine);

            _enabled = false;
        }
        public bool IsCurrentValueInPressRange()
        {
            return _currentValue >= _minPressValue && _currentValue <= _maxPressValue;
        }

        private void PlaceRange()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            CalculatePressRangeValues();

            _coroutine = _coroutineRunner.StartCoroutine(MoveSliderCoroutine(_sliderSpeed));

            _pressRangeImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _pressRangeWidth);
            SetPositionX(_minPressValue);
        }

        private IEnumerator MoveSliderCoroutine(float speed)
        {
            _timeController.SetSlowMotionTimeScale();
            speed /= _timeController.SlowMotionTimeScale;

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

        private void SetPositionX(float x)
        {
            Vector2 position = _pressRangeImage.rectTransform.anchoredPosition;
            position.x = x;
            _pressRangeImage.rectTransform.anchoredPosition = position;
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
