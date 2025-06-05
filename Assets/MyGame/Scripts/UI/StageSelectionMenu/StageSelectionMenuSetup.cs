using Base.Data;
using Base.Data.Game;
using System;
using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuSetup : MonoBehaviour
    {
        [SerializeField] private StageSelectionMenuView _view;

        private StageSelectionMenuModel _model;
        private StageSelectionMenuPresenter _presenter;

        public void Create(StagesData stages, GameSettings gameSettings)
        {
            _view.Init(stages, gameSettings.SelectedStage.Name);

            _model = new StageSelectionMenuModel(stages, gameSettings);
            _presenter = new StageSelectionMenuPresenter(_view, _model);
            _presenter.Enable();
        }
    }
}
