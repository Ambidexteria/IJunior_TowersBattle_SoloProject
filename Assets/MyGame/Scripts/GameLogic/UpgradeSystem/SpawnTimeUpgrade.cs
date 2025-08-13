using System;

namespace Base.GameLogic.UpgradeSystem
{
    [Serializable]
    public class SpawnTimeUpgrade : Upgrade
    {
        public SpawnTimeUpgrade(int currentLevel, int maxLevel, float upgradeValue) : base(currentLevel, maxLevel, upgradeValue)
        {
        }
    }
}
