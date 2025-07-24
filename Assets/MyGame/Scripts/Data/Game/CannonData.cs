using System;

namespace Base.Data.Game
{
    [Serializable]
    public class CannonData
    {
        public float MaxHealth = 50f;
        public float MaxEnergy = 50f;
        public int Damage = 10;

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
