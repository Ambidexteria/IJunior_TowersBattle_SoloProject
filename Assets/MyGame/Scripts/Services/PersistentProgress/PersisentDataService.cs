using Base.Data;
using Base.Data.Player;
using Base.GameLogic.UpgradeSystem;

namespace Base.Services.PersistentProgress
{
    public class PersisentDataService : IPersisentDataService
    {
        public PlayerProgress PlayerProgress { get; set; }
        public AudioVolumeSettings AudioVolumeSettings { get; set; }
        public Upgrades Upgrades { get; set; } = new Upgrades();
    }
}
