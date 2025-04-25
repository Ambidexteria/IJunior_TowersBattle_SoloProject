using Base.Data;
using UnityEngine;

namespace Base.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        public PlayerProgress LoadProgress()
        {
            return new(SceneNames.Initial);
        }

        public void SaveProgress(PlayerProgress progress)
        {
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}