using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonPresenter
    {
        private readonly CannonModel _model;
        private readonly CannonHealthView _healthView;

        public CannonPresenter(CannonModel model, CannonHealthView healthView)
        {
            _model = model;
            _healthView = healthView;

            _model.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float amount)
        {
            _healthView.Display(amount);
        }
    }
}
