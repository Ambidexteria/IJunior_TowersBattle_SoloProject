using Base.Data;
using Base.Data.Game;
using Base.Infrastructure;
using Base.PLayer;
using Base.Services.Audio;
using Base.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject.SpaceFighter;

namespace Base.GameLogic
{
    public class BattleEndSetup : MonoBehaviour
    {
        [SerializeField] private BattleEndView _view;

        private BattleEndPresenter _presenter;
        private BattleEndModel _model;

        public BattleEndModel Create(Game game, Wallet wallet, PlayerScore score, ISaveLoadService saveLoadService, int winReward, int defeatReward,
            StagesData stagesData, AudioPlayerService audioPlayer)
        {
            _model = new BattleEndModel(game, wallet, saveLoadService, winReward, defeatReward, score, stagesData, audioPlayer);

            _presenter = new BattleEndPresenter(_model, _view);
            _view.Enable();
            _presenter.Enable();

            return _model;
        }
    }
}
