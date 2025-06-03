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
        public CannonHealthUpgrade HealthUpgrade = new(1, 10, 10);
        public SoldierData SoldierData = new();
        public CannonData CannonData = new();
        public AudioVolumeSettings AudioVolumeSettings = new();
        public int CurrentGold = 10000;
    }

    [Serializable]
    public class SoldierData
    {
        public float MaxHealth = 20f;
        public float Speed = 5f;
        public float Damage = 2f;
        public float SpawnDelay = 5f;
    }

    [Serializable]
    public class CannonData
    {
        public float MaxHealth = 100f;
        public float MaxEnergy = 5f;
        public int Damage = 15;
    }
}
