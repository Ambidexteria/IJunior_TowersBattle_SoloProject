using Base.PLayer;
using Base.Services.SaveLoad;
using UnityEngine;

namespace Base.GameLogic
{
    public class BattleEndSetup : MonoBehaviour
    {
        [SerializeField] private BattleEndView _view;

        private BattleEndPresenter _presenter;
        private BattleEndModel _model;

        public BattleEndModel Create(Wallet wallet, ISaveLoadService saveLoadService)
        {
            _model = new BattleEndModel(wallet, saveLoadService);

            _presenter = new BattleEndPresenter(_model, _view);
            _presenter.Enable();

            return _model;
        }
    }
}
