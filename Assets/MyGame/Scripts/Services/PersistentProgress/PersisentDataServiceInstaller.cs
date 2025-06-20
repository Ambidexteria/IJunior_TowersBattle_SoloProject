using Zenject;

namespace Base.Services.PersistentProgress
{
    public class PersisentDataServiceInstaller : Installer<PersisentDataServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IPersisentDataService>().FromInstance(new PersisentDataService()).AsSingle();
        }
    }
}