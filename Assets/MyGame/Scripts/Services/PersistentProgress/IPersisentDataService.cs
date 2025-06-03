using Base.Data;
using Base.Data.Player;
using Base.GameLogic.UpgradeSystem;

namespace Base.Services.PersistentProgress
{
    public interface IPersisentDataService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}
