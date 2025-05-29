using System;
using UnityEngine;

namespace Base.GameLogic
{
    public class BattleEndPresenter
    {
        private readonly BattleEndModel _model;
        private readonly BattleEndView _view;

        public BattleEndPresenter(BattleEndModel model, BattleEndView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.BattleEnded += OnBattleEnded;
            _model.PlayerWinned += OnPlayerWinned;
            _model.PlayerLoosed += OnPlayerLoosed;

            _view.HomeButtonClicked += OnHomeButtonClicked;
        }

        public void Disable()
        {
            _model.BattleEnded -= OnBattleEnded;
            _model.PlayerWinned -= OnPlayerWinned;
            _model.PlayerLoosed -= OnPlayerLoosed;

            _view.HomeButtonClicked -= OnHomeButtonClicked;
        }

        private void OnHomeButtonClicked()
        {
            Debug.Log("BattleEndPresenter --- OnHomeButtonClicked");
            _model.LoadMainMenu();
        }

        private void OnBattleEnded(int earnedGold)
        {
            _view.ShowCurrentGold(_model.CurrentGoldAmount);
            _view.ShowEarnedGold(earnedGold);
        }

        private void OnPlayerWinned()
        {
            _view.ShowWinMessage();
        }

        private void OnPlayerLoosed()
        {
            _view.ShowDefeatMessage();
        }
    }
}
