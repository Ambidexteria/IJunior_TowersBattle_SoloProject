using System;

namespace Base.Data.Player
{
    [Serializable]
    public class PlayerProgress
    {
        public SoldierData SoldierData = new();
        public CannonData CannonData = new();
        public int CurrentGold;
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
        public float MaxEnergy = 20f;
        public int Damage = 15;
    }
}
