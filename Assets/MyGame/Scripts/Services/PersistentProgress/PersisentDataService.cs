using Base.Data;
using Base.Data.Player;
using Base.GameLogic.UpgradeSystem;

namespace Base.Services.PersistentProgress
{
    public class PersisentDataService : IPersisentDataService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}
