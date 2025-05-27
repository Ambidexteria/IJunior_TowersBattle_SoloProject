using Base.Data;
using Base.Data.Player;

namespace Base.Services.PersistentProgress
{
    public class PersisentDataService : IPersisentDataService
    {
        public PlayerProgress PlayerProgress { get; set; }
        public AudioVolumeSettings AudioVolumeSettings { get; set; }
    }
}
