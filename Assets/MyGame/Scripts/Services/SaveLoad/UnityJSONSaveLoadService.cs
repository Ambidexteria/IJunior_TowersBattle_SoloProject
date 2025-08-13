using Base.Data;
using Base.Data.Game;
using Base.Services.PersistentProgress;
using UnityEngine;

namespace Base.Services.SaveLoad
{
    public class UnityJSONSaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersisentDataService _dataService;

        public UnityJSONSaveLoadService(IPersisentDataService persisentDataService)
        {
            _dataService = persisentDataService;
        }

        public GameData LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<GameData>();
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetString(ProgressKey, _dataService.GameData.ToJson());
        }
    }
}