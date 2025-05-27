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
        }
        
        public void Disable()
        {
            _model.BattleEnded -= OnBattleEnded;
        }

        private void OnBattleEnded(int earnedGold)
        {
            _view.ShowCurrentGold(_model.CurrentGoldAmount);
            _view.ShowEarnedGold(earnedGold);
        }
    }
}
