using Base.Data;
using Base.Services.Factories.Game;
using Base.Services.PersistentProgress;
using UnityEngine;

namespace Base.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersisentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersisentProgressService persisentProgressService, IGameFactory gameFactory)
        {
            _progressService = persisentProgressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            foreach(ISavedProgress writer in _gameFactory.GetProgressWriters())
                writer.SaveProgress(_progressService.PlayerProgress);

            PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
        }
    }
}