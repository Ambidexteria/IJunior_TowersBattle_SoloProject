using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Base.GameLogic.UpgradeSystem
{
    [Serializable]
    public class Upgrades
    {
        [JsonRequired]
        private readonly Dictionary<Type, Upgrade> _upgrades;

        public Upgrades()
        {
            _upgrades = new Dictionary<Type, Upgrade>
            {
                { typeof(CannonHealthUpgrade), new CannonHealthUpgrade(0, 20, 10) },
                { typeof(CannonDamageUpgrade), new CannonDamageUpgrade(0, 10, 5) },
                { typeof(SpawnTimeUpgrade), new SpawnTimeUpgrade(0, 5, -1f) },
                { typeof(SoldierDamageUpgrade), new SoldierDamageUpgrade(0, 5, 0.2f) },
                { typeof(SoldierHealthUpgrade), new SoldierHealthUpgrade(0, 5, 3f) },
            };
        }

        public Type GetUpgrade<Type>() where Type 
            : Upgrade
        {
            Type upgrade = null;

            if (_upgrades.ContainsKey(typeof(Type)))
                upgrade = (Type)_upgrades[typeof(Type)];

            return upgrade;
        }
    }
}
