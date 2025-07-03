using Base.GameLogic.UpgradeSystem;
using Base.PLayer;
using Base.Services.SaveLoad;
using System;
using YG;

namespace Base.Shop
{
    public class ShopModel
    {
        private const string Coin = "Coin";
        private const int RewardCoins = 500;

        private readonly Wallet _wallet;
        private readonly RegularUpgradeSystem _upgradeSystem;
        private readonly ISaveLoadService _saveLoadService;
        private readonly UpgradePrices _prices;

        public ShopModel(Wallet wallet, RegularUpgradeSystem upgradeSystem, ISaveLoadService saveLoadService, 
            UpgradePrices prices)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(ShopModel), wallet, upgradeSystem, saveLoadService, prices);

            _wallet = wallet;
            _upgradeSystem = upgradeSystem;
            _saveLoadService = saveLoadService;
            _prices = prices;
        }

        public int CannonHealthUpgradePrice => _prices.CannonHealth;
        public int CannonDamageUpgradePrice => _prices.CannonDamage;
        public int SpawnTimeUpgradePrice => _prices.SpawnTime;
        public int SoldierDamageUpgradePrice => _prices.SoldierDamage;
        public int SoldierHealthUpgradePrice => _prices.SoldierHealth;

        public event Action<int> CurrentGoldChanged;

        public event Action<string> CannonDamageUpgradeLevelChanged;
        public event Action<string> HealthUpgradeLevelChanged;
        public event Action<string> SpawnTimehUpgradeLevelChanged;
        public event Action<string> SoldierDamageUpgradeLevelChanged;
        public event Action<string> SoldierHealthUpgradeLevelChanged;

        public void Enable()
        {
            CurrentGoldChanged?.Invoke(_wallet.CurrentAmount);

            UpdateUpgradeLevels();
        }

        public void ShowRewardAds()
        {
            YG2.RewardedAdvShow(Coin, AddReward);
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
            if (_wallet.IsEnoughMoney(SpawnTimeUpgradePrice))
            {
                if (_upgradeSystem.TryDecreseSpawnTime())
                {
                    BuyUpgrade(SpawnTimeUpgradePrice);
                }
            }
        }
        
        public void BuySoldierDamageUpgrade()
        {
            if (_wallet.IsEnoughMoney(SoldierDamageUpgradePrice))
            {
                if (_upgradeSystem.TryIncreaseSoldierDamage())
                {
                    BuyUpgrade(SoldierDamageUpgradePrice);
                }
            }
        }

        public void BuySoldierHealthUpgrade()
        {
            if (_wallet.IsEnoughMoney(SoldierHealthUpgradePrice))
            {
                if (_upgradeSystem.TryIncreaseSoldierHealth())
                {
                    BuyUpgrade(SoldierHealthUpgradePrice);
                }
            }
        }

        private void AddReward()
        {
            _wallet.Add(RewardCoins);
            _saveLoadService.SaveProgress();
            CurrentGoldChanged?.Invoke(_wallet.CurrentAmount);
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
            SoldierDamageUpgradeLevelChanged?.Invoke(_upgradeSystem.GetUpgradeLevel<SoldierDamageUpgrade>());
            SoldierHealthUpgradeLevelChanged?.Invoke(_upgradeSystem.GetUpgradeLevel<SoldierHealthUpgrade>());
        }
    }
}
