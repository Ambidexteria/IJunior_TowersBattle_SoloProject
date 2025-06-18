using Base.Data.Game;
using Base.Services.PersistentProgress;
using Newtonsoft.Json;
using UnityEngine;
using YG;

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
            ExceptionsTest.NullRefConstructorTest(nameof(NewtonsoftJSONSaveLoadService), progressService);

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
            GameData progress = _dataService.GameData;

            string jsonProgress = JsonConvert.SerializeObject(progress, Formatting.Indented, _settings);

            PlayerPrefs.SetString(ProgressKey, jsonProgress);
        }
    }
}
