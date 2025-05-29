using Base.GameLogic.UpgradeSystem;
using Base.PLayer;
using Base.Services.SaveLoad;
using System;
using System.Collections.Generic;

namespace Base.Shop
{
    [Serializable]
    public class UpgradePrices
    {
        public int CannonHealth = 100;
        public int CannonDamage = 100;
        public int SpawnTime = 100;
    }

    public class ShopModel
    {
        private readonly Wallet _wallet;
        private readonly RegularUpgradeSystem _upgradeSystem;
        private readonly ISaveLoadService _saveLoadService;
        private readonly UpgradePrices _prices;

        public int CannonHealthUpgradePrice => _prices.CannonHealth;

        public ShopModel(Wallet wallet, RegularUpgradeSystem upgradeSystem, ISaveLoadService saveLoadService, 
            UpgradePrices prices)
        {
            _wallet = wallet;
            _upgradeSystem = upgradeSystem;
            _saveLoadService = saveLoadService;
            _prices = prices;
        }

        public event Action<int> CurrentGoldChanged;

        public event Action<string> HealthUpgradeLevelChanged;

        public void Enable()
        {
            CurrentGoldChanged?.Invoke(_wallet.CurrentAmount);

            HealthUpgradeLevelChanged?.Invoke(_upgradeSystem.HealthUpgradeLevel);
        }

        public void BuyCannonHealthUpgrade()
        {
            if (_wallet.IsEnoughMoney(CannonHealthUpgradePrice))
            {
                if (_upgradeSystem.TryIncreaseCannonHealth(out string currentUpgradeLevel))
                {
                    _wallet.TryRemove(CannonHealthUpgradePrice);
                    HealthUpgradeLevelChanged?.Invoke(currentUpgradeLevel);
                    CurrentGoldChanged?.Invoke(_wallet.CurrentAmount);
                    _saveLoadService.SaveProgress();
                }
            }
        }
    }
}
