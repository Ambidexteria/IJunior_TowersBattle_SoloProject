using Base.Infrastructure;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarSetup : MonoBehaviour
    {
        [SerializeField] private CannonEnergyBarView _view;

        private CannonEnergyBarPresenter _presenter;
        private CannonEnergyBarModel _model;

        public CannonEnergyBarModel CreateCannonEnergyBar(Team team, ControlPointDatabase controlPointDatabase, 
            float maxEnergy, ICoroutineRunner coroutineRunner)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonEnergyBarSetup), ExceptionsTest.ConstructorName, team, controlPointDatabase, coroutineRunner);

            _model = new(team, controlPointDatabase, maxEnergy, coroutineRunner);

            _presenter = new CannonEnergyBarPresenter(_model, _view);
            _presenter.Enable();

            return _model;
        }
    }
}
