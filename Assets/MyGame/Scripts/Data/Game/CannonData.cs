using System;

namespace Base.Data.Game
{
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
