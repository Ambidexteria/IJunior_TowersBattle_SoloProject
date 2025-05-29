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
        private readonly int _maxLevel;

        public int CurrentLevel = 0;
        public int UpgradeValue = 10;

        public string CurrentLevelText => $"{CurrentLevel}/{_maxLevel}";

        public Upgrade(int currentLevel, int maxLevel, int upgradeValue)
        {
            CurrentLevel = currentLevel;
            _maxLevel = maxLevel;
            UpgradeValue = upgradeValue;
        }

        public bool TryIncreaseLevel()
        {
            if (CurrentLevel < _maxLevel)
            {
                CurrentLevel++;
                return true;
            }

            return false;
        }
    }

    public class RegularUpgradeSystem
    {
        private readonly IPersisentDataService _persisentDataService;
        private readonly HealthUpgrade _healthUpgrade;

        public string HealthUpgradeLevel => _healthUpgrade.CurrentLevelText;

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
