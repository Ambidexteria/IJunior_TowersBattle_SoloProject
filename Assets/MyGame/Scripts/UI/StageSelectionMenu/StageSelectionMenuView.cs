using Base.Data;
using System;
using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuView : MonoBehaviour
    {
        [SerializeField] private StageIconView[] _stageViews;

        public event Action<string> OnStageSelected;

        public void Init(StagesData stagesData)
        {
            StageInfo[] stages = stagesData.GetAllStages();

            for (int i = 0; i < _stageViews.Length; i++)
            {
                _stageViews[i].Init(stages[i].Unlocked, stages[i].Name);
                _stageViews[i].Choosed += OnStageChoosed;
            };
        }

        private void OnStageChoosed(string name)
        {
            OnStageSelected?.Invoke(name);
        }
    }
}
