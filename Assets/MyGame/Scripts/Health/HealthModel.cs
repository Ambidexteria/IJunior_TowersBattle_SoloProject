using Base.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

namespace Base.Health
{
    public class HealthModel
    {
        private readonly float _maxValue;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _smoothDecreasingSpeed;

        private float _currentHealth;
        private Coroutine _valueChanger;

        public HealthModel(float maxValue, ICoroutineRunner coroutineRunner, float smoothDecreasingSpeed = 1f)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(HealthModel), coroutineRunner);

            _maxValue = maxValue;
            _coroutineRunner = coroutineRunner;
            _smoothDecreasingSpeed = smoothDecreasingSpeed;
            Current = _maxValue;
        }

        public event Action Dying;
        public event Action<float> Changed;

        public float Current
        {
            get
            {
                return _currentHealth;
            }
            private set
            {
                if (value <= 0)
                    _currentHealth = 0;
                else
                    _currentHealth = value;
            }
        }

        public float MaxValue => _maxValue;
        public bool IsDead => Current <= 0;

        public void Increase(float amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount) + " in " + nameof(HealthModel));

            if (_valueChanger != null)
                _coroutineRunner.EndCoroutine(_valueChanger);

            Current += amount;
            Current = Mathf.Clamp(Current, 0, _maxValue);

            Changed?.Invoke(Current);
        }

        public void Decrease(float amount)
        {
            if (CanTakeDamage(amount) == false)
                return;

            Current -= amount;

            Changed?.Invoke(Current);

            if (IsDead)
                Dying?.Invoke();
        }

        public void SmoothDecrease(float amount)
        {
            if (CanTakeDamage(amount) == false)
                return;

            float startValue = Current;
            Current -= amount;

            if (_valueChanger != null)
                _coroutineRunner.EndCoroutine(_valueChanger);

            _valueChanger = _coroutineRunner.LaunchCoroutine(ChangeValueCoroutine(startValue, Current));

            if (IsDead)
                Dying?.Invoke();
        }

        private bool CanTakeDamage(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount) + " in " + nameof(HealthModel));

            return IsDead == false && amount >= 0;
        }

        private IEnumerator ChangeValueCoroutine(float startValue, float targetValue)
        {
            float changingSpeed = (startValue - targetValue) * _smoothDecreasingSpeed;
            float value = startValue;

            while (value != targetValue)
            {
                value = Mathf.MoveTowards(value, targetValue, changingSpeed * Time.deltaTime);
                Changed?.Invoke(value);

                yield return null;
            }
        }
    }
}