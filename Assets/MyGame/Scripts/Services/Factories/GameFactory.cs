using Base.Data;
using Base.Services.AssetManagment;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Base.Services.Factories
{
    public class GameFactory : IService
    {
        private const string HUDPath = "UI/MainMenuUI";

        private AssetLoader _assetLoader;

        private List<ISavedProgressReader> _progressReaders = new List<ISavedProgressReader>();
        private List<ISavedProgress> _progressWriters = new List<ISavedProgress>();

        public GameFactory(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;

        }

        public void CreateHUD()
        {
            GameObject hud = _assetLoader.Instantiate(HUDPath);

            foreach (ISavedProgressReader reader in hud.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(reader);
            }
        }

        public List<ISavedProgress> GetProgressWriters()
        {
            return new List<ISavedProgress>(_progressWriters);
        }

        public List<ISavedProgressReader> GetProgressReaders()
        {
            return new List<ISavedProgressReader>(_progressReaders);
        }

        public void Cleanup()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                _progressWriters.Add(progressWriter);

            _progressReaders.Add(progressReader);

            Debug.Log("READER REGISTERED");
        }
    }
}