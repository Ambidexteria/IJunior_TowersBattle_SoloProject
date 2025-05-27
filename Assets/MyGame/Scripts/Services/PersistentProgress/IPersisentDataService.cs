using Base.Data;
using Base.Data.Player;

namespace Base.Services.PersistentProgress
{
    public interface IPersisentDataService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
        AudioVolumeSettings AudioVolumeSettings { get; set; }
    }
}
