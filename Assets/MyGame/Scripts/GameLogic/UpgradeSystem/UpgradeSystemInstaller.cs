using Zenject;

namespace Base.GameLogic.UpgradeSystem
{
    public class UpgradeSystemInstaller : Installer<UpgradeSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<RegularUpgradeSystem>().AsSingle();
        }
    }
}