using Zenject;

namespace Base.Infrastructure
{
    public class GameMonoInstaller : MonoInstaller<GameMonoInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<Game>().AsSingle();
        }
    }
}