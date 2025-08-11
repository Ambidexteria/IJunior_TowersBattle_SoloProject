using Base.Data;
using Base.Data.Game;
using Base.Infrastructure;
using Base.Services.SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuSetup : MonoBehaviour
    {
        [SerializeField] private StageIconsDatabase _iconsDatabase;
        [SerializeField] private StageIconSetup _iconPrefab;
        [SerializeField] private RectTransform _iconsParentObject;

        private StageSelectionMenu _model;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(StageSelectionMenuSetup), nameof(Awake),
                _iconPrefab, _iconsParentObject);
        }

        public void Create(StagesData stages, GameSettings gameSettings, ISaveLoadService saveLoadService, Game game)
        {
            ExceptionsTest.NullRefMethodTest(nameof(StageSelectionMenuSetup), nameof(Create), stages, gameSettings, saveLoadService);

            List<StageIconModel> icons = new();

            foreach (var stageInfo in stages.UnlockedStagesInfo)
            {
                StageIconSetup setup = Instantiate(_iconPrefab);
                setup.transform.SetParent(_iconsParentObject);
                icons.Add(setup.CreateModel(_iconsDatabase.GetStageIcon(stageInfo.IconName), stageInfo.Unlocked, stageInfo.StageName));
            }

            _model = new StageSelectionMenu(icons.ToArray(), stages, gameSettings, saveLoadService, game);
        }
    }
}
