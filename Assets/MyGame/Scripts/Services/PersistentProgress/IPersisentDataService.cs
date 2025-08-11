using Base.Data.Game;

namespace Base.Services.PersistentProgress
{
    public interface IPersisentDataService : IService
    {
        public GameData GameData { get; set; }
    }
}
