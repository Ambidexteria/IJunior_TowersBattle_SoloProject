using System;

namespace Base.GameLogic.UpgradeSystem
{
    [Serializable]
    public class CannonHealthUpgrade : Upgrade
    {
        public CannonHealthUpgrade(int currentLevel, int maxLevel, float upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }
}
