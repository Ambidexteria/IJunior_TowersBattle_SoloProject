using Base.Health;
using Base.Infrastructure;
using UnityEngine;
using System;

namespace Base
{
    public class HealthSetup : MonoBehaviour
    {
        [SerializeField] private HealthView _view;

        private HealthModel _model;
        private HealthPresenter _presenter;

        public HealthModel GetModel()
        {
            if(_model == null )
                throw new NullReferenceException();

            return _model;
        }

        public HealthModel CreateHealth(float maxHealth, ICoroutineRunner coroutineRunner)
        {
            _model = new HealthModel(maxHealth, coroutineRunner);

            _presenter = new HealthPresenter(_model, _view);
            _presenter.Enable();

            return _model;
        }
    }
}
