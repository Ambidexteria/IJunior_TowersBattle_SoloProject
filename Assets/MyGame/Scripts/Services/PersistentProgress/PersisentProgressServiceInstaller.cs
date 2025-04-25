using Zenject;

namespace Base.Services.PersistentProgress
{
    public class PersisentProgressServiceInstaller : Installer<PersisentProgressServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IPersisentProgressService>().FromInstance(new PersisentProgressService()).AsSingle();
        }
    }
}