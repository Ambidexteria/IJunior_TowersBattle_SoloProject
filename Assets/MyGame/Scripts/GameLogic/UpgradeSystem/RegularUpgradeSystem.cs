using Base.Services.PersistentProgress;
using Base.Services.PluginYG;

namespace Base.GameLogic.UpgradeSystem
{
    public class RegularUpgradeSystem
    {
        private readonly IPersisentDataService _dataService;
        private readonly Upgrades _upgrades;


        public RegularUpgradeSystem(IPersisentDataService dataService)
        {
            _dataService = dataService;
        }

        public string GetUpgradeLevel<Type>() where Type : Upgrade
        {
            return _dataService.GameData.PlayerData.Upgrades.GetUpgrade<Type>().CurrentLevelText;
        }

        public bool TryIncreaseCannonHealth()
        {
            CannonHealthUpgrade upgrade = GetUpgrades().GetUpgrade<CannonHealthUpgrade>();

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
            CannonDamageUpgrade upgrade = GetUpgrades().GetUpgrade<CannonDamageUpgrade>();

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
            SpawnTimeUpgrade upgrade = GetUpgrades().GetUpgrade<SpawnTimeUpgrade>();

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
            SoldierDamageUpgrade upgrade = GetUpgrades().GetUpgrade<SoldierDamageUpgrade>();

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
            SoldierHealthUpgrade upgrade = GetUpgrades().GetUpgrade<SoldierHealthUpgrade>();

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

        private Upgrades GetUpgrades()
        {
            return _dataService.GameData.PlayerData.Upgrades;
        }

        private void SendMetrics(string upgradeName, string level)
        {
            MetricsService.CallUpgradeBoughtEvent(upgradeName, level);
        }
    }
}
