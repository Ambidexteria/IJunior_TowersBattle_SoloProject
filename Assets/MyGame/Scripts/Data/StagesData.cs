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
        //[JsonRequired]
        public StageInfo[] _stages/* = new()*/;
        //{
        //    new StageInfo("Stages/Stage (1)", "1", true, 200, 50, new SoldierData(10, 4, 1, 10f), new CannonData(30, 30, 10)),
        //    new StageInfo("Stages/Stage (2)", "2", true, 400, 100, new SoldierData(15, 4, 1, 8f), new CannonData(50, 25, 15)),
        //    new StageInfo("Stages/Stage (3)", "3", true, 600, 150, new SoldierData(20, 4, 1, 6f), new CannonData(70, 20, 20))
        //};

        public StagesData()
        {
            Debug.Log($"{nameof(StagesData)} - constructor");
            //Debug.Log($"{nameof(_stages)}.Count = {_stages.Count}");

            _stages = new[]
            {
                new StageInfo("Stages/Stage (1)", "1", true, 200, 50, new SoldierData(10, 4, 1, 10f), new CannonData(30, 30, 10)),
                new StageInfo("Stages/Stage (2)", "2", true, 400, 100, new SoldierData(15, 4, 1, 8f), new CannonData(50, 25, 15)),
                new StageInfo("Stages/Stage (3)", "3", true, 600, 150, new SoldierData(20, 4, 1, 6f), new CannonData(70, 20, 20))
            };

            Debug.Log($"{nameof(_stages)}.Length = {_stages.Length}");
        }

        public string SelectedStageName = string.Empty;

        public StageInfo GetSelectedStage()
        {
            StageInfo stageInfo = null;

            if (SelectedStageName == string.Empty)
                stageInfo = _stages[0];
            else
                TryGetStageByName(SelectedStageName, out stageInfo);

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
