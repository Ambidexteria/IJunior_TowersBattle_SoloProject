using Base.Infrastructure;
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

        public BattleEndModel Create(Game game, Wallet wallet, ISaveLoadService saveLoadService)
        {
            _model = new BattleEndModel(game, wallet, saveLoadService);

            _presenter = new BattleEndPresenter(_model, _view);
            _view.Enable();
            _presenter.Enable();

            return _model;
        }
    }
}
