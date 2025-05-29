using Base.Services.PersistentProgress;
using System;

namespace Base.PLayer
{
    public class Wallet
    {
        private readonly IPersisentDataService _progressService;

        private int _currentValue;

        public Wallet(IPersisentDataService progressService)
        {
            _progressService = progressService;
            _currentValue = _progressService.PlayerProgress.CurrentGold;
        }

        public int CurrentAmount => _currentValue;

        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currentValue += amount;
            UpdateGold();
        }

        public bool TryRemove(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (amount <= _currentValue)
            {
                _currentValue -= amount;
                UpdateGold();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsEnoughMoney(int price)
        {
            return _currentValue >= price;
        }

        private void UpdateGold()
        {
            _progressService.PlayerProgress.CurrentGold = _currentValue;
        }
    }
}