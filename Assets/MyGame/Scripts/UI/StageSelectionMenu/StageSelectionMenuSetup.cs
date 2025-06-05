using Base.Data;
using Base.Data.Game;
using System;
using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuPresenter
    {
        private readonly StageSelectionMenuView _view;
        private readonly StageSelectionMenuModel _model;

        public StageSelectionMenuPresenter(StageSelectionMenuView view, StageSelectionMenuModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.OnStageSelected += OnStageSelected;
        }

        private void OnStageSelected(string name)
        {
            _model.SetActiveStage(name);
        }
    }

    public class StageSelectionMenuModel
    {
        private readonly StagesData _stagesData;
        private readonly GameSettings _gameSettings;

        public StageSelectionMenuModel(StagesData stagesData, GameSettings gameSettings)
        {
            _stagesData = stagesData;
            _gameSettings = gameSettings;
        }

        public void SetActiveStage(string name)
        {
            if(_stagesData.TryGetStageByName(name, out StageInfo stageInfo))
                _gameSettings.SelectedStage = stageInfo;
        }
    }

    public class StageSelectionMenuSetup : MonoBehaviour
    {
        [SerializeField] private StageSelectionMenuView _view;

        private StageSelectionMenuModel _model;
        private StageSelectionMenuPresenter _presenter;

        public void Create(StagesData stages, GameSettings gameSettings)
        {
            _view.Init(stages);

            _model = new StageSelectionMenuModel(stages, gameSettings);
            _presenter = new StageSelectionMenuPresenter(_view, _model);
            _presenter.Enable();
        }
    }
}
