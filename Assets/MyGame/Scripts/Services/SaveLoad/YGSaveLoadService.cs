using Base.Data.Game;
using Base.Services.PersistentProgress;
using Newtonsoft.Json;
using UnityEngine;
using YG;

namespace Base.Services.SaveLoad
{
    public class YGSaveLoadService : ISaveLoadService
    {
        private readonly IPersisentDataService _dataService;
        private readonly JsonSerializerSettings _settings;

        public YGSaveLoadService(IPersisentDataService dataService)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(YGSaveLoadService), dataService);

            _dataService = dataService;

            _settings = new()
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
        }

        public GameData LoadProgress()
        {
            GameData gameData = null;

            if (YG2.saves.ResetProgress)
            {
                YG2.saves.ResetProgress = false;
                YG2.SaveProgress();

                return gameData;
            }

            string json = YG2.saves.JSONGameData ??= string.Empty;
            Debug.Log(json);

            if (json != string.Empty)
            {
                if (json[1] == 'n')
                {
                    Debug.LogWarning("Replacing chars");
                    json = ConvertJsonString(json);
                }
            }

            Debug.Log(json);
            Debug.Log($"gameData string hashCode = {json.GetHashCode()}");
            Debug.Log($"string length = {json.Length}");

            try
            {
                gameData = JsonConvert.DeserializeObject<GameData>(json, _settings);
            }
            catch (JsonSerializationException)
            {
                Debug.LogError($"{nameof(JsonSerializationException)} catched. Handling...");
                gameData = null;
            }

            return gameData;
        }

        public void SaveProgress()
        {
            GameData gameData = _dataService.GameData;
            string json = JsonConvert.SerializeObject(gameData, Formatting.Indented, _settings);

            Debug.Log(json);
            Debug.Log($"gameData string hashCode = {json.GetHashCode()}");
            Debug.Log($"string length = {json.Length}");

            YG2.saves.JSONGameData = JsonConvert.SerializeObject(gameData, _settings);
            YG2.SaveProgress();
        }

        private string ConvertJsonString(string json)
        {
            char previous;
            char next;

            for (int i = 0; i < json.Length; i++)
            {
                if (json[i] == 'n')
                {
                    previous = json[i - 1];
                    next = json[i + 1];
                    if (previous == '{' || previous == '}' || previous == ',' || next == ' ')
                    {
                        json = json.Remove(i, 1);
                        json = json.Insert(i, "\n");
                    }
                }
            }

            return json;
        }
    }
}
