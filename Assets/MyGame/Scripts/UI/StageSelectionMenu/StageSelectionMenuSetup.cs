using Base.Data;
using Base.Data.Game;
using Base.Services.SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuSetup : MonoBehaviour
    {
        [SerializeField] private StageIconSetup _iconPrefab;
        [SerializeField] private RectTransform _iconsParentObject;

        private StageSelectionMenu _model;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(StageSelectionMenuSetup), nameof(Awake),
                _iconPrefab, _iconsParentObject);
        }

        public void Create(StagesData stages, GameSettings gameSettings, ISaveLoadService saveLoadService)
        {
            ExceptionsTest.NullRefMethodTest(nameof(StageSelectionMenuSetup), nameof(Create), stages, gameSettings, saveLoadService);

            List<StageIconModel> icons = new List<StageIconModel>();

            foreach (var stageInfo in stages.UnlockedStagesInfo)
            {
                StageIconSetup setup = Instantiate(_iconPrefab);
                setup.transform.SetParent(_iconsParentObject);
                icons.Add(setup.CreateModel(stageInfo.Value, stageInfo.Key));
            }

            _model = new StageSelectionMenu(icons.ToArray(), stages, gameSettings, saveLoadService);
        }
    }
}
