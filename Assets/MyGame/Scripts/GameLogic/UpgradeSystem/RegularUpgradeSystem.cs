using Base.Services.PersistentProgress;
using System;

namespace Base.GameLogic.UpgradeSystem
{
    [Serializable]
    public class HealthUpgrade : Upgrade
    {
        public HealthUpgrade(int currentLevel, int maxLevel, int upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }

    [Serializable]
    public abstract class Upgrade
    {
        private int _currentLevel = 1;
        private int _maxLevel = 10;
        private int _upgradeValue = 10;

        public int UpgradeValue => _upgradeValue;
        public string CurrentLevelText => $"{_currentLevel}/{_maxLevel}";

        protected Upgrade(int currentLevel, int maxLevel, int upgradeValue)
        {
            _currentLevel = currentLevel;
            _maxLevel = maxLevel;
            _upgradeValue = upgradeValue;
        }

        public bool TryIncreaseLevel()
        {
            if(_currentLevel < _maxLevel)
            {
                _currentLevel++;
                return true;
            }

            return false;
        }
    }

    public class RegularUpgradeSystem
    {
        private readonly IPersisentDataService _persisentDataService;

        private readonly HealthUpgrade _healthUpgrade;

        public RegularUpgradeSystem(IPersisentDataService persisentDataService)
        {
            _persisentDataService = persisentDataService;

            _healthUpgrade = _persisentDataService.PlayerProgress.HealthUpgrade;
        }

        public bool TryIncreaseCannonHealth(out string currentUpgradeLevel)
        {
            currentUpgradeLevel = null;

            if (_healthUpgrade.TryIncreaseLevel())
            {
                _persisentDataService.PlayerProgress.CannonData.MaxHealth += _healthUpgrade.UpgradeValue;
                currentUpgradeLevel = _healthUpgrade.CurrentLevelText;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
