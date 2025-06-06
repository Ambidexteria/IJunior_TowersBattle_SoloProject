using Base.Data.Game;
using Base.Services.PersistentProgress;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Base.Services.SaveLoad
{
    public class NewtonsoftJSONSaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private const string UpgradesKey = "Upgrades";

        private readonly IPersisentDataService _dataService;
        private readonly JsonSerializerSettings _settings;

        public NewtonsoftJSONSaveLoadService(IPersisentDataService progressService)
        {
            _dataService = progressService;

            _settings = new()
            {
                TypeNameHandling = TypeNameHandling.All,
            };
        }

        public GameData LoadProgress()
        {
            string progress = PlayerPrefs.GetString(ProgressKey);

            return JsonConvert.DeserializeObject<GameData>(progress, _settings);
        }

        public void SaveProgress()
        {
            GameData progress = _dataService.PlayerProgress;
            Debug.Log("Progress saved");

            string jsonProgress = JsonConvert.SerializeObject(progress, Formatting.Indented, _settings);

            PlayerPrefs.SetString(ProgressKey, jsonProgress);
        }
    }
}
