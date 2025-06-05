using Base.Data;
using Base.Data.Game;
using Base.GameLogic.UpgradeSystem;

namespace Base.Services.PersistentProgress
{
    public interface IPersisentDataService : IService
    {
        public GameData PlayerProgress { get; set; }
    }
}
