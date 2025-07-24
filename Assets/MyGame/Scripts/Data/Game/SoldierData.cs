using System;

namespace Base.Data.Game
{
    [Serializable]
    public class SoldierData
    {
        public float MinDistanceToTarget  = 2f;
        public float BrakeSpeed = 4f;
        public float MaxHealth = 10f;
        public float Speed = 4f;
        public float Damage = 1.2f;
        public float SpawnDelay = 12f;

        public SoldierData()
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
}
