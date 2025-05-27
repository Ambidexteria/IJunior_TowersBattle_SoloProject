using Base.PLayer;
using Base.Services.SaveLoad;
using System;

namespace Base.GameLogic
{
    public class BattleEndModel
    {
        private const int EarnedGold = 100;

        private readonly Wallet _wallet;
        private readonly ISaveLoadService _saveLoadService;

        public int CurrentGoldAmount => _wallet.CurrentAmount;

        public BattleEndModel(Wallet wallet, ISaveLoadService saveLoadService)
        {
            _wallet = wallet;
            _saveLoadService = saveLoadService;
        }

        public event Action PlayerWinned;
        public event Action PlayerLoosed;
        public event Action<int> BattleEnded;

        public void End(bool isPlayerWin)
        {
            _wallet.Add(EarnedGold);
            BattleEnded?.Invoke(EarnedGold);

            _saveLoadService.SaveProgress();
        }
    }
}
