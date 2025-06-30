using Base.Data.Game;
using System;

namespace Base.Data
{
    [Serializable]
    public class StageInfo
    {
        public string AssetPath;
        public string Name;
        public int WinReward;
        public int DefeatReward;
        public SoldierData EnemySoldier;
        public CannonData EnemyCannon;

        public StageInfo()
        {
        }

        public StageInfo(string assetPath, string name, int winReward, int defeatReward,
            SoldierData enemySoldier, CannonData enemyCannon)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(StageInfo), assetPath, name, enemySoldier, enemyCannon);

            AssetPath = assetPath;
            Name = name;
            WinReward = winReward;
            DefeatReward = defeatReward;
            EnemySoldier = enemySoldier;
            EnemyCannon = enemyCannon;
        }

        public StageInfo Clone()
        {
            return new StageInfo(AssetPath, Name, WinReward, DefeatReward, EnemySoldier, EnemyCannon);
        }
    }
}
