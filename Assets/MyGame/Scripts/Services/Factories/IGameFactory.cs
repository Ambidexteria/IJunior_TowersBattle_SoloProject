using Base.Data;
using System.Collections.Generic;

namespace Base.Services.Factories
{
    public interface IGameFactory
    {
        void CreateHUD();
        void Cleanup();
        List<ISavedProgressReader> GetProgressReaders();
        List<ISavedProgress> GetProgressWriters();
    }
}