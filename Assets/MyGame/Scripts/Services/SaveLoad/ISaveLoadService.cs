using Base.Data;

namespace Base.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress(PlayerProgress progress);
        PlayerProgress LoadProgress();
    }
}