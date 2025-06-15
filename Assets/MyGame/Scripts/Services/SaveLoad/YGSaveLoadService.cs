using Base.Data.Game;
using Base.Services.PersistentProgress;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Services.SaveLoad
{
    public class YGSaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersisentDataService _dataService;
        private readonly JsonSerializerSettings _settings;

        public YGSaveLoadService(IPersisentDataService progressService)
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
            GameData data = _dataService.GameData;

            string jsonConvertedData = JsonConvert.SerializeObject(data, Formatting.Indented, _settings);

            PlayerPrefs.SetString(ProgressKey, jsonConvertedData);
        }

    }
}
