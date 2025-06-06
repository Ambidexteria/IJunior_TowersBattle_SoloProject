using Base.Data.Game;
using System;

namespace Base.Data
{
    [Serializable]
    public class StageInfo
    {
        public string AssetPath;
        public string Name;
        public bool Unlocked;
        public int WinReward;
        public int DefeatReward;
        public SoldierData EnemySoldier;
        public CannonData EnemyCannon;

        public StageInfo()
        {
        }

        public StageInfo(string assetPath, string name, bool unlocked, int winReward, int defeatReward,
            SoldierData enemySoldier, CannonData enemyCannon)
        {
            AssetPath = assetPath;
            Name = name;
            Unlocked = unlocked;
            WinReward = winReward;
            DefeatReward = defeatReward;
            EnemySoldier = enemySoldier;
            EnemyCannon = enemyCannon;
        }

        public StageInfo Clone()
        {
            return new StageInfo(AssetPath, Name, Unlocked, WinReward, DefeatReward, EnemySoldier, EnemyCannon);
        }
    }
}
