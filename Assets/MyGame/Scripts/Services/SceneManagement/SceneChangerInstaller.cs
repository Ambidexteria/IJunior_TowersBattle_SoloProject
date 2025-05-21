using Zenject;

namespace Base.Services.SceneManagment
{
    public class SceneChangerInstaller : Installer<SceneChangerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneChanger>().FromNew().AsSingle();
        }
    }
}