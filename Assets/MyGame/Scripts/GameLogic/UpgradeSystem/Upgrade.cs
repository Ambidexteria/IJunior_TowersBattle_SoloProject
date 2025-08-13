using Newtonsoft.Json;
using System;

namespace Base.GameLogic.UpgradeSystem
{
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
}
