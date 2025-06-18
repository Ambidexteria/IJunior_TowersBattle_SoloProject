using Base.Data.Game;
using Base.Infrastructure;
using Base.PLayer;
using Base.Services.SaveLoad;
using System;

namespace Base.GameLogic
{
    public class BattleEndModel
    {
        private readonly Game _game;
        private readonly Wallet _wallet;
        private readonly ISaveLoadService _saveLoadService;
        private readonly int _winReward;
        private readonly int _defeatReward;
        private readonly PlayerScore _score;
        private int _earnedGold;

        public int CurrentGoldAmount => _wallet.CurrentAmount;

        public BattleEndModel(Game game, Wallet wallet, ISaveLoadService saveLoadService, int winReward, int defeatReward, 
            PlayerScore score)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(BattleEndModel), game, wallet, saveLoadService, score);

            _game = game;
            _wallet = wallet;
            _saveLoadService = saveLoadService;
            _winReward = winReward;
            _defeatReward = defeatReward;
            _score = score;
        }

        public event Action PlayerWinned;
        public event Action PlayerLoosed;
        public event Action<int> ScoreChanged;
        public event Action<int> GoldAmountChanged;

        public void End(bool isPlayerWin, int npcCannonDamageTaken)
        {

            if (isPlayerWin)
            {
                _earnedGold = _winReward;
                PlayerWinned?.Invoke();
            }
            else
            {
                _earnedGold = _defeatReward;
                PlayerLoosed?.Invoke();
            }

            _wallet.Add(_earnedGold);
            _score.Value += npcCannonDamageTaken;
            GoldAmountChanged?.Invoke(_earnedGold);
            ScoreChanged?.Invoke(npcCannonDamageTaken);

            _saveLoadService.SaveProgress();
        }

        public void LoadMainMenu()
        {
            _game.LoadMainMenu();
        }
    }
}
