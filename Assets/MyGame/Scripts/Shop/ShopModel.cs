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
        public int CannonDamageUpgradePrice => _prices.CannonDamage;
        public int SpawnTimeUpgradePrice => _prices.SpawnTime;

        public ShopModel(Wallet wallet, RegularUpgradeSystem upgradeSystem, ISaveLoadService saveLoadService, 
            UpgradePrices prices)
        {
            _wallet = wallet;
            _upgradeSystem = upgradeSystem;
            _saveLoadService = saveLoadService;
            _prices = prices;
        }

        public event Action<int> CurrentGoldChanged;

        public event Action<string> CannonDamageUpgradeLevelChanged;
        public event Action<string> HealthUpgradeLevelChanged;
        public event Action<string> SpawnTimehUpgradeLevelChanged;

        public void Enable()
        {
            CurrentGoldChanged?.Invoke(_wallet.CurrentAmount);

            UpdateUpgradeLevels();
        }

        public void BuyCannonHealthUpgrade()
        {
            if (_wallet.IsEnoughMoney(CannonHealthUpgradePrice))
            {
                if (_upgradeSystem.TryIncreaseCannonHealth())
                {
                    BuyUpgrade(CannonHealthUpgradePrice);
                }
            }
        }

        public void BuyCannonDamageUpgrade()
        {
            if (_wallet.IsEnoughMoney(CannonDamageUpgradePrice))
            {
                if (_upgradeSystem.TryIncreaseCannonDamage())
                {
                    BuyUpgrade(CannonDamageUpgradePrice);
                }
            }
        }

        public void BuySpawnTimeUpgrade()
        {
            if (_wallet.IsEnoughMoney(CannonDamageUpgradePrice))
            {
                if (_upgradeSystem.TryDecreseSpawnTime())
                {
                    BuyUpgrade(CannonDamageUpgradePrice);
                }
            }
        }

        private void BuyUpgrade(int price)
        {
            _wallet.TryRemove(price);
            UpdateUpgradeLevels();
            CurrentGoldChanged?.Invoke(_wallet.CurrentAmount);
            _saveLoadService.SaveProgress();
        }

        private void UpdateUpgradeLevels()
        {
            HealthUpgradeLevelChanged?.Invoke(_upgradeSystem.GetUpgradeLevel<CannonHealthUpgrade>());
            CannonDamageUpgradeLevelChanged?.Invoke(_upgradeSystem.GetUpgradeLevel<CannonDamageUpgrade>());
            SpawnTimehUpgradeLevelChanged?.Invoke(_upgradeSystem.GetUpgradeLevel<SpawnTimeUpgrade>());
        }
    }
}
