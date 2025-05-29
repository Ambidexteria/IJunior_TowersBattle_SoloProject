using Base.GameLogic.UpgradeSystem;
using Base.PLayer;
using Base.Services.SaveLoad;
using System;

namespace Base.Shop
{
    public class ShopModel
    {
        private readonly Wallet _wallet;
        private readonly RegularUpgradeSystem _upgradeSystem;
        private readonly ISaveLoadService _saveLoadService;

        private int _cannonHealthUpgradePrice = 100;

        public ShopModel(Wallet wallet, RegularUpgradeSystem upgradeSystem, ISaveLoadService saveLoadService)
        {
            _wallet = wallet;
            _upgradeSystem = upgradeSystem;
            _saveLoadService = saveLoadService;
        }

        public event Action<int> CurrentGoldChanged;

        public event Action<string> HealthUpgradeLevelChanged;

        public void Enable()
        {
            CurrentGoldChanged?.Invoke(_wallet.CurrentAmount);
        }

        public void BuyCannonHealthUpgrade()
        {
            if (_wallet.IsEnoughMoney(_cannonHealthUpgradePrice))
            {
                if (_upgradeSystem.TryIncreaseCannonHealth(out string currentUpgradeLevel))
                {
                    _wallet.TryRemove(_cannonHealthUpgradePrice);
                    HealthUpgradeLevelChanged?.Invoke(currentUpgradeLevel);
                    CurrentGoldChanged?.Invoke(_wallet.CurrentAmount);
                    _saveLoadService.SaveProgress();
                }
            }
        }
    }
}
