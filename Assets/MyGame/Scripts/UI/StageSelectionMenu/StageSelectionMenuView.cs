using Base.Data;
using System;
using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuView : MonoBehaviour
    {
        [SerializeField] private StageIconView[] _stageViews;

        private StageIconView _selectedIcon;

        public event Action<string> StageSelected;

        public void Init(StagesData stagesData, string selectedStage)
        {
            StageInfo[] stages = stagesData.GetAllStages();

            for (int i = 0; i < _stageViews.Length; i++)
            {

                _stageViews[i].Init(stages[i].Unlocked, stages[i].Name);
                _stageViews[i].Clicked += OnStageChoosed;

                if(stages[i].Name == selectedStage)
                    _selectedIcon = _stageViews[i];
            };

            SetActiveStageIcon(selectedStage);
        }

        public void SetActiveStageIcon(string stageName)
        {
            _selectedIcon.HideBorder();

            foreach (var stage in _stageViews)
            {
                if (stage.StageName == stageName)
                {
                    stage.ShowBorder();
                    break;
                }
            }
        }

        private void OnStageChoosed(StageIconView stageIconView)
        {
            _selectedIcon = stageIconView;
            StageSelected?.Invoke(_selectedIcon.StageName);
        }
    }
}
