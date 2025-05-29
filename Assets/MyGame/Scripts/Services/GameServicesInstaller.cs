using Base.GameLogic.UpgradeSystem;
using Base.PLayer;
using Zenject;

namespace Base.Services
{
    public class GameServicesInstaller : MonoInstaller
    {
        private DiContainer _container;

        public override void InstallBindings()
        {
            _container = ProjectContext.Instance.Container;

            WalletInstaller.Install(_container);
            UpgradeSystemInstaller.Install(_container);
        }
    }
}
