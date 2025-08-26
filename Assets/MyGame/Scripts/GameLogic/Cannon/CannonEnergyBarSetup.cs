using Base.Infrastructure;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarSetup : MonoBehaviour
    {
        [SerializeField] private CannonEnergyBarView _view;

        private CannonEnergyBarPresenter _presenter;
        private CannonEnergyBarModel _model;

        public CannonEnergyBarModel CreateCannonEnergyBar(
            Team team, 
            ControlPointDatabase controlPointDatabase, 
            float maxEnergy, 
            ICoroutineRunner coroutineRunner)
        {
            _model = new CannonEnergyBarModel(team, controlPointDatabase, maxEnergy, coroutineRunner);

            _presenter = new CannonEnergyBarPresenter(_model, _view);
            _presenter.Enable();

            return _model;
        }
    }
}
