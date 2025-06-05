using Base.Data;
using Base.Data.Game;

namespace Base.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        GameData LoadProgress();
    }
}