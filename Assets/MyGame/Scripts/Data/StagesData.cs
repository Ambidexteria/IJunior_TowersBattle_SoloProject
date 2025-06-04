using Base.Data.Player;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Base.Data
{
    [Serializable]
    public class StagesData
    {
        [JsonRequired]
        private List<StageInfo> _stages;

        public StagesData()
        {
            _stages = new List<StageInfo>();

            AddFirstStage();
            AddSecondStage();
            AddThridStage();
        }

        public bool TryGetStageByName(string name, out StageInfo stageInfo)
        {
            stageInfo = null;

            foreach (var stage in _stages)
            {
                if (stage.Name == name)
                {
                    stageInfo = stage;
                    return true;
                }
            }

            return false;
        }

        public StageInfo[] GetAllStages()
        {
            return _stages.ToArray();
        }

        private void AddFirstStage()
        {
            SoldierData soldierData = new SoldierData(15, 4, 1, 10f);
            CannonData cannonData = new CannonData(30, 30, 10);

            StageInfo stage = new StageInfo("Stages/Stage (1)", "1", true, 200, 50, soldierData, cannonData);

            _stages.Add(stage);
        }

        private void AddSecondStage()
        {
            SoldierData soldierData = new SoldierData(15, 4, 1, 10f);
            CannonData cannonData = new CannonData(40, 30, 10);

            StageInfo stage = new StageInfo("Stages/Stage (2)", "2", true, 300, 70, soldierData, cannonData);

            _stages.Add(stage);
        }
        
        private void AddThridStage()
        {
            SoldierData soldierData = new SoldierData(15, 4, 1, 10f);
            CannonData cannonData = new CannonData(50, 25, 15);

            StageInfo stage = new StageInfo("Stages/Stage (3)", "3", true, 400, 100, soldierData, cannonData);

            _stages.Add(stage);
        }
    }

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
    }
}
