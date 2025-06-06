using Base.Data;
using Base.Data.Game;
using Base.Services.SaveLoad;
using System;
using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuSetup : MonoBehaviour
    {
        [SerializeField] private StageIconSetup _iconPrefab;
        [SerializeField] private RectTransform _iconsParentObject;
        [SerializeField] private StageSelectionMenuView _view;

        private StageSelectionMenuModel _model;
        private StageSelectionMenuPresenter _presenter;

        public void Create(StagesData stages, GameSettings gameSettings, ISaveLoadService saveLoadService)
        {
            var stagesInfo = stages.GetAllStages();
            StageIconModel[] iconModels = new StageIconModel[stagesInfo.Length];

            for (int i = 0; i < stagesInfo.Length; i++)
            {
                StageIconSetup setup = Instantiate(_iconPrefab);
                setup.transform.SetParent(_iconsParentObject);
                iconModels[i] = setup.CreateModel(stagesInfo[i].Unlocked, stagesInfo[i].Name);
            }

            _model = new StageSelectionMenuModel(iconModels, stages, gameSettings, saveLoadService);
        }
    }
}
