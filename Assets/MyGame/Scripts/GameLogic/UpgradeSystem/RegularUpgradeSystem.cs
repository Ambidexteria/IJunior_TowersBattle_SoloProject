using Base.Services.PersistentProgress;
using Base.Services.PluginYG;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Base.GameLogic.UpgradeSystem
{
    [Serializable]
    public class Upgrades
    {
        [JsonRequired]
        public Dictionary<Type, Upgrade> _upgrades;

        public Upgrades()
        {
            _upgrades = new Dictionary<Type, Upgrade>
            {
                { typeof(CannonHealthUpgrade), new CannonHealthUpgrade(0, 15, 10) },
                { typeof(CannonDamageUpgrade), new CannonDamageUpgrade(0, 6, 5) },
                { typeof(SpawnTimeUpgrade), new SpawnTimeUpgrade(0, 5, -1f) },
                { typeof(SoldierDamageUpgrade), new SoldierDamageUpgrade(0, 5, 0.2f) },
                { typeof(SoldierHealthUpgrade), new SoldierHealthUpgrade(0, 5, 3f) }
            };
        }

        public Type GetUpgrade<Type>() where Type : Upgrade
        {
            Type upgrade = null;

            if (_upgrades.ContainsKey(typeof(Type)))
                upgrade = (Type)_upgrades[typeof(Type)];

            return upgrade;
        }
    }

    [Serializable]
    public class CannonHealthUpgrade : Upgrade
    {
        public CannonHealthUpgrade(int currentLevel, int maxLevel, float upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }

    [Serializable]
    public class CannonDamageUpgrade : Upgrade
    {
        public CannonDamageUpgrade(int currentLevel, int maxLevel, float upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }

    [Serializable]
    public class SpawnTimeUpgrade : Upgrade
    {
        public SpawnTimeUpgrade(int currentLevel, int maxLevel, float upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }

    public class SoldierDamageUpgrade : Upgrade
    {
        public SoldierDamageUpgrade(int currentLevel, int maxLevel, float upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }  
    
    public class SoldierHealthUpgrade : Upgrade
    {
        public SoldierHealthUpgrade(int currentLevel, int maxLevel, float upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }

    [Serializable]
    public abstract class Upgrade
    {
        [JsonRequired]
        private readonly int _maxLevel;
        [JsonRequired]
        private readonly float _upgradeValue;
        [JsonRequired]
        private int _currentLevel = 0;

        public float UpgradeValue => _upgradeValue;

        public string CurrentLevelText => $"{_currentLevel}/{_maxLevel}";

        public Upgrade(int currentLevel, int maxLevel, float upgradeValue)
        {
            _currentLevel = currentLevel;
            _maxLevel = maxLevel;
            _upgradeValue = upgradeValue;
        }

        public bool TryIncreaseLevel()
        {
            if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
                return true;
            }

            return false;
        }
    }

    public class RegularUpgradeSystem
    {
        private readonly IPersisentDataService _dataService;
        private readonly Upgrades _upgrades;
        private readonly CannonHealthUpgrade _healthUpgrade;

        public string HealthUpgradeLevel => _healthUpgrade.CurrentLevelText;

        public RegularUpgradeSystem(IPersisentDataService dataService)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(RegularUpgradeSystem), dataService);

            _dataService = dataService;

            _upgrades = _dataService.GameData.PlayerData.Upgrades;
            _healthUpgrade = _upgrades.GetUpgrade<CannonHealthUpgrade>();
        }

        public string GetUpgradeLevel<Type>() where Type : Upgrade
        {
            return _upgrades.GetUpgrade<Type>().CurrentLevelText;
        }

        public bool TryIncreaseCannonHealth()
        {
            CannonHealthUpgrade upgrade = _upgrades.GetUpgrade<CannonHealthUpgrade>();

            if (upgrade.TryIncreaseLevel())
            {
                _dataService.GameData.PlayerData.CannonData.MaxHealth += upgrade.UpgradeValue;
                SendMetrics(nameof(CannonHealthUpgrade), upgrade.CurrentLevelText);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryIncreaseCannonDamage()
        {
            CannonDamageUpgrade upgrade = _upgrades.GetUpgrade<CannonDamageUpgrade>();

            if (upgrade.TryIncreaseLevel())
            {
                _dataService.GameData.PlayerData.CannonData.Damage += (int)upgrade.UpgradeValue;
                SendMetrics(nameof(CannonDamageUpgrade), upgrade.CurrentLevelText);

                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool TryDecreseSpawnTime()
        {
            SpawnTimeUpgrade upgrade = _upgrades.GetUpgrade<SpawnTimeUpgrade>();

            if (upgrade.TryIncreaseLevel())
            {
                _dataService.GameData.PlayerData.SoldierData.SpawnDelay += upgrade.UpgradeValue;
                SendMetrics(nameof(SpawnTimeUpgrade), upgrade.CurrentLevelText);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryIncreaseSoldierDamage()
        {
            SoldierDamageUpgrade upgrade = _upgrades.GetUpgrade<SoldierDamageUpgrade>();

            if (upgrade.TryIncreaseLevel())
            {
                _dataService.GameData.PlayerData.SoldierData.Damage += upgrade.UpgradeValue;
                SendMetrics(nameof(SoldierDamageUpgrade), upgrade.CurrentLevelText);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryIncreaseSoldierHealth()
        {
            SoldierHealthUpgrade upgrade = _upgrades.GetUpgrade<SoldierHealthUpgrade>();

            if (upgrade.TryIncreaseLevel())
            {
                _dataService.GameData.PlayerData.SoldierData.MaxHealth += upgrade.UpgradeValue;
                SendMetrics(nameof(SoldierHealthUpgrade), upgrade.CurrentLevelText);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void SendMetrics(string upgradeName, string level)
        {
            MetricsService.CallUpgradeBoughtEvent(upgradeName, level);
        }
    }
}
