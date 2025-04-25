using Base.Data;

namespace Base.Services.PersistentProgress
{
    public interface IPersisentProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}
