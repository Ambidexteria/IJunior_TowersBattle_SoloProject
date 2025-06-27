using Base.Data.Game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Data
{
    [Serializable]
    public class StagesData
    {
        private const string _stageOne = "1";
        private const string _stageTwo = "2";
        private const string _stageTree = "3";

        public bool[] UnlockedStages = new bool[]
        {
            true,
            false,
            false
        };

        [JsonRequired]
        public Dictionary<string, bool> UnlockedStagesDictionary = new Dictionary<string, bool>()
        {
            {_stageOne, true},
            {_stageTwo, false},
            {_stageTree, false},
        };

        //[JsonRequired]
        public StageInfo[] _stages;

        public StagesData()
        {
            _stages = new[]
            {
                new StageInfo("Stages/Stage (1)", _stageOne, true, 200, 50, new SoldierData(10, 4, 1, 10f), new CannonData(30, 30, 10)),
                new StageInfo("Stages/Stage (2)", _stageTwo, false, 400, 100, new SoldierData(15, 4, 1, 8f), new CannonData(50, 25, 15)),
                new StageInfo("Stages/Stage (3)", _stageTree, false, 600, 150, new SoldierData(20, 4, 1, 6f), new CannonData(70, 20, 20))
            };
        }

        public string SelectedStageName = string.Empty;

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

            Debug.Log($"{nameof(StagesData)} - unlocked stages - array");

            for (int i = 0; i < UnlockedStages.Length; i++)
            {
                Debug.Log($"{i + 1} = {UnlockedStages[i]}");
            }
            
            Debug.Log($"{nameof(StagesData)} - unlocked stages - dictionary");

            foreach (var stage in UnlockedStagesDictionary)
            {
                Debug.Log($"{stage.Key} = {stage.Value}");
            }
            
        }

        public void TryUnlockNextStage()
        {
            for (int i = 0; i < _stages.Length; i++)
            {
                if (_stages[i].Name == SelectedStageName)
                {
                    if (i < _stages.Length - 1)
                    {
                        if (_stages[i + 1].Unlocked == false)
                        {
                            _stages[i + 1].Unlocked = true;
                            UnlockedStages[i + 1] = true;
                            UnlockedStagesDictionary[_stages[i + 1].Name] = true;
                        }
                    }
                }
            }
        }

        public StageInfo[] GetAllStages()
        {
            StageInfo[] stageInfos = new StageInfo[_stages.Length];

            for (int i = 0; i < stageInfos.Length; i++)
            {
                stageInfos[i] = _stages[i];
            }

            return stageInfos;
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
