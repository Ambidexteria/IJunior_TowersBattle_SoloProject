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
        [JsonRequired]
        private List<StageInfo> _stages = new()
        {
            new StageInfo("Stages/Stage (1)", "1", true, 200, 50, new SoldierData(15, 4, 1, 10f), new CannonData(30, 30, 10)),
            new StageInfo("Stages/Stage (2)", "2", true, 200, 50, new SoldierData(15, 4, 1, 10f), new CannonData(30, 30, 10)),
            new StageInfo("Stages/Stage (3)", "3", true, 200, 50, new SoldierData(15, 4, 1, 10f), new CannonData(30, 30, 10))
        };

        public string SelectedStageName = string.Empty;

        //[JsonRequired]
        //private bool _created = false;

        //public void Init()
        //{
        //    if(_created) 
        //        return;

        //    AddFirstStage();
        //    AddSecondStage();
        //    AddThridStage();
        //    AddFourthStage();
        //    AddFourthStage();

        //    if (_selectedStage == null)
        //        _selectedStage = _stages[0];

        //    Debug.Log($"{nameof(StagesData)} --- {nameof(_selectedStage)} --- {_selectedStage.Name}");
        //    //_selectedStage = _stages[0];

        //    _created = true;
        //}

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
            StageInfo[] stageInfos = new StageInfo[_stages.Count];

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

        private void AddFourthStage()
        {
            SoldierData soldierData = new SoldierData(15, 4, 1, 10f);
            CannonData cannonData = new CannonData(50, 25, 15);

            StageInfo stage = new StageInfo("Stages/Stage (3)", "3", false, 400, 100, soldierData, cannonData);

            _stages.Add(stage);
        }
    }
}
