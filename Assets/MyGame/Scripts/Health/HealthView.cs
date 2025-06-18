using System;
using TMPro;
using UnityEngine;

namespace Base.Health
{
    public class HealthView : MonoBehaviour
    {
        private float _maxHealth;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private SliderValueChanger _healthBar;        

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(HealthView), nameof(Awake), _text, _healthBar);
        }

        public void Display(float value)
        {
            string text = $"{(int)value} / {(int)GetMaxHealth()}";
            _text.text = text;
            _healthBar.SetValue(value);
        }

        public void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            _healthBar.SetMinMaxValues(0, _maxHealth);
            Display(_maxHealth);
        }

        public float GetMaxHealth()
        {
            return _maxHealth;
        }
    }
}