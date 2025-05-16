using System;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public abstract class CannonHealthView : MonoBehaviour
    {
        private float _maxHealth;

        private void Awake()
        {
            PrepareOnAwake();
        }

        public void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            Display(_maxHealth);
        }

        public float GetMaxHealth()
        {
            return _maxHealth;
        }

        public abstract void Display(float value);

        public abstract void PrepareOnAwake();
    }
}