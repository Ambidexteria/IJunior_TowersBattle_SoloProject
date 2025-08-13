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
            _model.GoldAmountChanged += OnGoldEarned;
            _model.ScoreChanged += OnScoreChanged;
            _model.PlayerWinned += OnPlayerWinned;
            _model.PlayerLoosed += OnPlayerLoosed;
            _model.NextStageUnlocked += OnNextStageUnlocked;

            _view.HomeButtonClicked += OnHomeButtonClicked;
            _view.NextStageButtonClicked += OnNextStageButtonClicked;
        }

        public void Disable()
        {
            _model.GoldAmountChanged -= OnGoldEarned;
            _model.ScoreChanged -= OnScoreChanged;
            _model.PlayerWinned -= OnPlayerWinned;
            _model.PlayerLoosed -= OnPlayerLoosed;
            _model.NextStageUnlocked -= OnNextStageUnlocked;

            _view.HomeButtonClicked -= OnHomeButtonClicked;
            _view.NextStageButtonClicked -= OnNextStageButtonClicked;
        }

        private void OnHomeButtonClicked()
        {
            _model.LoadMainMenu();
        }

        private void OnNextStageButtonClicked()
        {
            _model.LoadNextStage();
        }

        private void OnGoldEarned(int earnedGold)
        {
            _view.ShowEarnedGold(earnedGold);
        }

        private void OnScoreChanged(int score)
        {
            _view.ShowScore(score);
        }

        private void OnPlayerWinned()
        {
            _view.ShowWinMessage();
        }

        private void OnPlayerLoosed()
        {
            _view.ShowDefeatMessage();
        }

        private void OnNextStageUnlocked()
        {
            _view.ShowNextStageButton();
        }
    }
}
