using Base.Data.Game;
using Newtonsoft.Json;
using System;

namespace Base.Data
{
    public struct SerializedStageInfo
    {
        public string StageName;
        public string IconName;
        public bool Unlocked;

        public SerializedStageInfo(string stageName, string iconName, bool unlocked)
        {
            StageName = stageName;
            IconName = iconName;
            Unlocked = unlocked;
        }

        public void Unlock()
        {
            Unlocked = true;
        }
    }

    [Serializable]
    public class StagesData
    {
        private const string ForrestIcon = "forest_lake_icon";
        private const string DesertIcon = "desert_icon";
        private const string SnowForestIcon = "snow_forest_icon";

        private const string StageOne = "1";
        private const string StageTwo = "2";
        private const string StageThree = "3";
        private const string StageFour = "4";
        private const string StageFive = "5";
        private const string StageSix = "6";
        private const string StageSeven = "7";
        private const string StageEight = "8";
        private const string StageNine = "9";

        private readonly StageInfo[] _stages;

        [JsonRequired]
        public SerializedStageInfo[] UnlockedStagesInfo;

        public string SelectedStageName = string.Empty;

        public StagesData()
        {
            _stages = new[]
            {
                new StageInfo("Stages/Stage (1)", StageOne, 200, 50, new SoldierData(15, 4, 1, 10f), new CannonData(30, 50, 10)),
                new StageInfo("Stages/Stage (2)", StageTwo, 250, 50, new SoldierData(15, 4, 1, 10f), new CannonData(40, 50, 10)),
                new StageInfo("Stages/Stage (3)", StageThree, 300, 50, new SoldierData(15, 4, 1, 10f), new CannonData(50, 50, 15)),
                new StageInfo("Stages/Stage (4)", StageFour, 500, 100, new SoldierData(15, 4, 2, 8f), new CannonData(70, 50, 15)),
                new StageInfo("Stages/Stage (5)", StageFive, 600, 100, new SoldierData(15, 4, 2, 8f), new CannonData(100, 50, 15)),
                new StageInfo("Stages/Stage (6)", StageSix, 700, 100, new SoldierData(15, 4, 2, 8f), new CannonData(120, 50, 20)),
                new StageInfo("Stages/Stage (7)", StageSeven, 1000, 150, new SoldierData(20, 4, 2.5f, 6f), new CannonData(150, 50, 25)),
                new StageInfo("Stages/Stage (8)", StageEight, 1200, 150, new SoldierData(20, 4, 2.5f, 6f), new CannonData(170, 50, 25)),
                new StageInfo("Stages/Stage (9)", StageNine, 1500, 150, new SoldierData(20, 4, 2.5f, 6f), new CannonData(200, 50, 30))
            };

            UnlockedStagesInfo = new SerializedStageInfo[]
            {
                new(StageOne, ForrestIcon, true),
                new(StageTwo, ForrestIcon, true),
                new(StageThree, ForrestIcon, true),
                new(StageFour, DesertIcon, true),
                new(StageFive, DesertIcon, true),
                new(StageSix, DesertIcon, true),
                new(StageSeven, SnowForestIcon, true),
                new(StageEight, SnowForestIcon, true),
                new(StageNine, SnowForestIcon, true),
            };
        }

        public StageInfo GetSelectedStage()
        {
            if (SelectedStageName == string.Empty)
                SelectedStageName = _stages[0].Name;

            TryGetStageByName(SelectedStageName, out StageInfo stageInfo);

            return stageInfo.Clone();
        }

        public bool IsStageExist(string name)
        {
            return TryGetStageByName(name, out _);
        }

        public void SetSelectedStage(string name)
        {
            if (IsStageExist(name))
                SelectedStageName = name;
        }

        public void ChangeStageToNextOne()
        {
            for (int i = 0; i < _stages.Length; i++)
            {
                if (_stages[i].Name == SelectedStageName)
                {
                    if (i + 1 < _stages.Length)
                    {
                        SelectedStageName = _stages[i + 1].Name;
                        break;
                    }
                }
            }
        }

        public bool UnlockNextStage()
        {
            for (int i = 0; i < _stages.Length; i++)
            {
                if (_stages[i].Name == SelectedStageName)
                {
                    if (i < _stages.Length - 1)
                    {
                        UnlockedStagesInfo[i + 1].Unlock();
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryGetStageByName(string name, out StageInfo stageInfo)
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
    }
}
