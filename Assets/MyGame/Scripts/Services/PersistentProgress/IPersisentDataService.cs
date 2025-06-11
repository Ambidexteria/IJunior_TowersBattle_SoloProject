using Base.Data;
using Base.Data.Game;
using Base.GameLogic.UpgradeSystem;

namespace Base.Services.PersistentProgress
{
    public interface IPersisentDataService : IService
    {
        public GameData GameData { get; set; }
    }
}
