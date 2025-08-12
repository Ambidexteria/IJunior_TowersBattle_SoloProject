using Zenject;

namespace Base.PLayer
{
    public class WalletInstaller : Installer<WalletInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Wallet>().AsSingle();
        }
    }
}
