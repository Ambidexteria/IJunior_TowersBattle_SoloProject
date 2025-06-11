using Base.Data;
using Base.Data.Game;
using Base.GameLogic.UpgradeSystem;

namespace Base.Services.PersistentProgress
{
    public class PersisentDataService : IPersisentDataService
    {
        public GameData GameData { get; set; }
    }
}
