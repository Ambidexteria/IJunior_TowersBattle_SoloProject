using Base.Health;
using Base.Infrastructure;
using UnityEngine;

namespace Base
{
    public class HealthSetup : MonoBehaviour
    {
        [SerializeField] private HealthView _view;

        private HealthModel _model;
        private HealthPresenter _presenter;
        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(HealthSetup), nameof(Awake), _view);
        }
        public HealthModel CreateHealth(float maxHealth, ICoroutineRunner coroutineRunner)
        {
            ExceptionsTest.NullRefMethodTest(nameof(HealthSetup), nameof(CreateHealth), coroutineRunner);

            _model = new HealthModel(maxHealth, coroutineRunner);

            _presenter = new HealthPresenter(_model, _view);
            _presenter.Enable();

            return _model;
        }
    }
}
