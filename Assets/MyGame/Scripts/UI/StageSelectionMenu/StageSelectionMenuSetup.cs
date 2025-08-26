using System.Collections.Generic;
using Base.Data;
using Base.Infrastructure;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Base.UI.StageSelection
{
    public class StageSelectionMenuSetup : MonoBehaviour
    {
        [SerializeField] private StageIconsDatabase _iconsDatabase;
        [SerializeField] private StageIconSetup _iconPrefab;
        [SerializeField] private RectTransform _iconsParentObject;

        private IPersisentDataService _persisentDataService;
        private ISaveLoadService _saveLoadService;
        private Game _game;

        private StageSelectionMenu _model;
        private List<StageIconSetup> _stageIconSetups;

        [Inject]
        private void Init(IPersisentDataService persisentDataService, ISaveLoadService saveLoadService, Game game)
        {
            _persisentDataService = persisentDataService;
            _saveLoadService = saveLoadService;
            _game = game;
            _stageIconSetups = new List<StageIconSetup>();
        }

        private void OnEnable()
        {
            CreateIcons();
        }

        private void OnDisable()
        {
            DestroyIcons();
        }

        private void CreateIcons()
        {
            List<StageIconModel> icons = new List<StageIconModel>();

            foreach (var stageInfo in _persisentDataService.GameData.StagesData.UnlockedStagesInfo)
            {
                StageIconSetup setup = Instantiate(_iconPrefab);
                _stageIconSetups.Add(setup);
                setup.transform.SetParent(_iconsParentObject);
                setup.transform.localScale = Vector3.one;

                icons.Add(setup.CreateModel(_iconsDatabase.GetStageIcon(stageInfo.IconName), stageInfo.Unlocked, stageInfo.StageName));
            }

            _model = new StageSelectionMenu(
                icons.ToArray(), 
                _persisentDataService.GameData.StagesData, 
                _persisentDataService.GameData.GameSettings, 
                _saveLoadService, 
                _game);
        }

        private void DestroyIcons()
        {
            foreach (var iconSetup in _stageIconSetups)
                Destroy(iconSetup.gameObject);

            _stageIconSetups?.Clear();
        }
    }
}
