using Base.Services.PersistentProgress;
using Unity.Plastic.Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Base.GameLogic.UpgradeSystem
{
    [Serializable]
    public class Upgrades
    {
        [JsonRequired]
        private Dictionary<Type, Upgrade> _upgrades;

        public Upgrades()
        {
            _upgrades = new Dictionary<Type, Upgrade>();
            _upgrades.Add(typeof(CannonHealthUpgrade), new CannonHealthUpgrade(0, 10, 10));
            _upgrades.Add(typeof(CannonDamageUpgrade), new CannonDamageUpgrade(0, 10, 5));
            _upgrades.Add(typeof(SpawnTimeUpgrade), new SpawnTimeUpgrade(0, 10, -0.5f));
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

    [Serializable]
    public abstract class Upgrade
    {
        [JsonRequired]
        private int _maxLevel;

        [JsonRequired]
        private float _upgradeValue;

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
            _dataService = dataService;

            _upgrades = _dataService.GameData.Upgrades;
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
                _dataService.GameData.CannonData.MaxHealth += upgrade.UpgradeValue;
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
                _dataService.GameData.CannonData.Damage += (int)upgrade.UpgradeValue;
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
                _dataService.GameData.SoldierData.SpawnDelay += upgrade.UpgradeValue;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
