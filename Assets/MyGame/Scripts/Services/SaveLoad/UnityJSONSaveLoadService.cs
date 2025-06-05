using Base.Data;
using Base.Data.Game;
using Base.Services.PersistentProgress;
using UnityEngine;

namespace Base.Services.SaveLoad
{
    public class UnityJSONSaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersisentDataService _progressService;

        public UnityJSONSaveLoadService(IPersisentDataService persisentProgressService)
        {
            _progressService = persisentProgressService;
        }

        public GameData LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<GameData>();
        }

        public void LoadUpgrades()
        {
            throw new System.NotImplementedException();
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
        }
    }
}