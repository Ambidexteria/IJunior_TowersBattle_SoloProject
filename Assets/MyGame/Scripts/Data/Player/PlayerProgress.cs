using Base.GameLogic.UpgradeSystem;
using Base.Shop;
using Newtonsoft.Json;
using System;

namespace Base.Data.Player
{
    [Serializable]
    public class PlayerProgress
    {
        [JsonRequired]
        public Upgrades Upgrades = new();
        public UpgradePrices UpgradePrices = new();
        public StagesData StagesData = new ();
        public GameSettings GameSettings = new();
        public SoldierData SoldierData = new();
        public CannonData CannonData = new();
        public AudioVolumeSettings AudioVolumeSettings = new();
        public int CurrentGold = 10000;
    }

    [Serializable]
    public class GameSettings
    {
        public StageInfo SelectedStage;
    }

    [Serializable]
    public class SoldierData
    {
        public float MaxHealth = 20f;
        public float Speed = 5f;
        public float Damage = 2f;
        public float SpawnDelay = 5f;

        public SoldierData ()
        {
        }

        public SoldierData(float maxHealth, float speed, float damage, float spawnDelay)
        {
            MaxHealth = maxHealth;
            Speed = speed;
            Damage = damage;
            SpawnDelay = spawnDelay;
        }
    }

    [Serializable]
    public class CannonData
    {
        public float MaxHealth = 100f;
        public float MaxEnergy = 5f;
        public int Damage = 15;

        public CannonData()
        {
        }

        public CannonData(float maxHealth, float maxEnergy, int damage)
        {
            MaxHealth = maxHealth;
            MaxEnergy = maxEnergy;
            Damage = damage;
        }
    }
}
