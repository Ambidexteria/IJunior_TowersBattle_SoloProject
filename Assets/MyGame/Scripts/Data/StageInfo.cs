using System;
using Base.Data.Game;

namespace Base.Data
{
    [Serializable]
    public class StageInfo
    {
        public string AssetPath;
        public string Name;
        public string IconName;
        public int WinReward;
        public int DefeatReward;
        public SoldierData EnemySoldier;
        public CannonData EnemyCannon;

        public StageInfo()
        {
        }

        public StageInfo(
            string assetPath, 
            string name, 
            string iconName, 
            int winReward, 
            int defeatReward,
            SoldierData enemySoldier, 
            CannonData enemyCannon)
        {
            AssetPath = assetPath;
            Name = name;
            IconName = iconName;
            WinReward = winReward;
            DefeatReward = defeatReward;
            EnemySoldier = enemySoldier;
            EnemyCannon = enemyCannon;
        }

        public StageInfo Clone()
        {
            return new StageInfo(AssetPath, Name, IconName, WinReward, DefeatReward, EnemySoldier, EnemyCannon);
        }
    }
}
