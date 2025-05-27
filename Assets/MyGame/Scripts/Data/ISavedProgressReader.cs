using Base.Data.Player;

namespace Base.Data
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress playerProgress);
    }
}
