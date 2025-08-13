using Base.Data.Game;
using Newtonsoft.Json;
using System;

namespace Base.Data
{
    [Serializable]
    public class StagesData
    {
        private const string ForrestIcon = "forest_lake_icon";
        private const string DesertIcon = "desert_icon";
        private const string SnowForestIcon = "snow_forest_icon";
        private const string BeachIcon = "beach_icon";

        private const string StageOne = "1";
        private const string StageTwo = "2";
        private const string StageThree = "3";
        private const string StageFour = "4";
        private const string StageFive = "5";
        private const string StageSix = "6";
        private const string StageSeven = "7";
        private const string StageEight = "8";
        private const string StageNine = "9";
        private const string StageTen = "10";
        private const string StageEleven = "11";
        private const string StageTwelve = "12";

        private readonly StageInfo[] _stages;

        [JsonRequired]
        public SerializedStageInfo[] UnlockedStagesInfo;

        public string SelectedStageName = string.Empty;

        public StagesData()
        {
            _stages = new[]
            {
                new StageInfo("Stages/Stage (1)", StageOne, ForrestIcon, 300, 50, new SoldierData(10, 4, 1, 12f), new CannonData(30, 50, 10)),
                new StageInfo("Stages/Stage (2)", StageTwo, ForrestIcon, 400, 50, new SoldierData(10, 4, 1, 12f), new CannonData(40, 50, 10)),
                new StageInfo("Stages/Stage (3)", StageThree, ForrestIcon, 500, 50, new SoldierData(10, 4, 1.2f, 11f), new CannonData(50, 50, 15)),
                new StageInfo("Stages/Stage (4)", StageFour, DesertIcon, 600, 100, new SoldierData(15, 4, 1.2f, 11f), new CannonData(70, 50, 15)),
                new StageInfo("Stages/Stage (5)", StageFive, DesertIcon, 700, 100, new SoldierData(15, 4, 1.2f, 11f), new CannonData(100, 50, 15)),
                new StageInfo("Stages/Stage (6)", StageSix ,DesertIcon, 800, 100, new SoldierData(15, 4, 1.4f, 10f), new CannonData(120, 50, 20)),
                new StageInfo("Stages/Stage (7)", StageSeven, SnowForestIcon, 900, 150, new SoldierData(20, 4, 1.4f, 10f), new CannonData(150, 50, 25)),
                new StageInfo("Stages/Stage (8)", StageEight, SnowForestIcon, 1000, 150, new SoldierData(20, 4, 1.4f, 10f), new CannonData(170, 50, 25)),
                new StageInfo("Stages/Stage (9)", StageNine, SnowForestIcon, 1100, 150, new SoldierData(20, 4, 1.6f, 9f), new CannonData(200, 50, 30)),
                new StageInfo("Stages/Stage (10)", StageTen, BeachIcon, 1200, 200, new SoldierData(25, 4, 1.6f, 9f), new CannonData(210, 50, 30)),
                new StageInfo("Stages/Stage (11)", StageEleven, BeachIcon, 1300, 200, new SoldierData(25, 4, 1.8f, 8f), new CannonData(230, 50, 30)),
                new StageInfo("Stages/Stage (12)", StageTwelve, BeachIcon, 1500, 200, new SoldierData(25, 4, 2.1f, 7f), new CannonData(250, 50, 40)),
            };

            UnlockedStagesInfo = new SerializedStageInfo[]
            {
                new(StageOne, ForrestIcon, true),
                new(StageTwo, ForrestIcon, false),
                new(StageThree, ForrestIcon, false),
                new(StageFour, DesertIcon, false),
                new(StageFive, DesertIcon, false),
                new(StageSix, DesertIcon, false),
                new(StageSeven, SnowForestIcon, false),
                new(StageEight, SnowForestIcon, false),
                new(StageNine, SnowForestIcon, false),
                new(StageTen, BeachIcon, false),
                new(StageEleven, BeachIcon, false),
                new(StageTwelve, BeachIcon, false),
            };
        }

        public void CheckForUpdate()
        {
            if (_stages.Length > UnlockedStagesInfo.Length)
            {
                for (int i = UnlockedStagesInfo.Length; i < _stages.Length; i++)
                {
                    ExpandUnlockedStagesInfo(new SerializedStageInfo(_stages[i].Name, _stages[i].IconName, false));
                }
            }
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

        private void ExpandUnlockedStagesInfo(SerializedStageInfo stageInfo)
        {
            SerializedStageInfo[] array = new SerializedStageInfo[UnlockedStagesInfo.Length + 1];

            for (int i = 0; i < UnlockedStagesInfo.Length; i++)
                array[i] = UnlockedStagesInfo[i];

            array[UnlockedStagesInfo.Length] = stageInfo;
            UnlockedStagesInfo = array;
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
