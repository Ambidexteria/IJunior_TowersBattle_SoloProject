using Base.Data.Game;

namespace Base.Services.PersistentProgress
{
    public class PersisentDataService : IPersisentDataService
    {
        public GameData GameData { get; set; }
    }
}
