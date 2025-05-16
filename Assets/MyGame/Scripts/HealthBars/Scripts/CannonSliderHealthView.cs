using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonSliderHealthView : CannonHealthView
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private SliderValueChanger _healthBar;
        [SerializeField] private float _changeSpeed = 0.2f;

        private Coroutine _valueChanger;

        public override void PrepareOnAwake()
        {
            if (_healthBar == null)
                throw new ArgumentNullException();

            if (_text == null)
                throw new ArgumentNullException();
        }


        public override void Display(float value)
        {
            string text = $"{(int)value} / {(int)GetMaxHealth()}";
            _text.text = text;

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
}