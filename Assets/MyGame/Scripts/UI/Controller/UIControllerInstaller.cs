using Zenject;

namespace Base.UI.Controller
{
    public class UIControllerInstaller : Installer<UIControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UIController>().FromNew().AsSingle();
        }
    }
}