namespace Base.Data
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void SaveProgress(PlayerProgress playerProgress);
    }
}
