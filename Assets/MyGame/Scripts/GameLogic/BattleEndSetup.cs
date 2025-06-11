using Base.Data.Game;
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

        public BattleEndModel Create(Game game, Wallet wallet, PlayerScore score, ISaveLoadService saveLoadService, int winReward, int defeatReward)
        {
            _model = new BattleEndModel(game, wallet, saveLoadService, winReward, defeatReward, score);

            _presenter = new BattleEndPresenter(_model, _view);
            _view.Enable();
            _presenter.Enable();

            return _model;
        }
    }
}
