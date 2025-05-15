using Base.Data;
using Base.UI.MainMenu;
using System.Collections.Generic;

namespace Base.Services.Factories.Game
{
    public interface IGameFactory
    {
        void Cleanup();
        List<ISavedProgressReader> GetProgressReaders();
        List<ISavedProgress> GetProgressWriters();
    }
}