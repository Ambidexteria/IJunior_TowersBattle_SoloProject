using Base.Infrastructure;
using Zenject;

public class GameInstaller : Installer<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        Container.Bind<Game>().AsSingle().NonLazy();
    }
}