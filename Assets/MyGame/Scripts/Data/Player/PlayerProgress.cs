using Base.GameLogic.UpgradeSystem;
using System;

namespace Base.Data.Player
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthUpgrade HealthUpgrade = new(1, 10, 10);
        public SoldierData SoldierData = new();
        public CannonData CannonData = new();
        public int CurrentGold = 500;
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
