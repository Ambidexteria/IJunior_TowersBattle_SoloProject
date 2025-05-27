using Base.Data;
using Base.Data.Player;
using Base.PLayer;
using Base.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersisentDataService _progressService;

        public SaveLoadService(IPersisentDataService persisentProgressService)
        {
            _progressService = persisentProgressService;
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
        }
    }
}