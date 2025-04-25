using Base.Infrastructure;
using Zenject;

namespace Base.Services.SceneManagment
{
    public class SceneLoaderInstaller : Installer<SceneLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().FromNew().AsSingle();
        }
    }
}