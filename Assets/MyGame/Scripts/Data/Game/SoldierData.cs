using System;

namespace Base.Data.Game
{
    [Serializable]
    public class SoldierData
    {
        public float MinDistanceToTarget  = 2f;
        public float BrakeSpeed = 3f;
        public float MaxHealth = 15f;
        public float Speed = 5f;
        public float Damage = 1f;
        public float SpawnDelay = 10f;

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
