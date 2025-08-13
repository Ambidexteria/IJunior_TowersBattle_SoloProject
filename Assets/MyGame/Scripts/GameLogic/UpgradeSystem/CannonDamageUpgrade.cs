using System;

namespace Base.GameLogic.UpgradeSystem
{
    [Serializable]
    public class CannonDamageUpgrade : Upgrade
    {
        public CannonDamageUpgrade(int currentLevel, int maxLevel, float upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }
}
