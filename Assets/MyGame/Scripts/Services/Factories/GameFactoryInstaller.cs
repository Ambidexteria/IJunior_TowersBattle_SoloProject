using Base.Services.Factories;
using Zenject;

public class GameFactoryInstaller : Installer<GameFactoryInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameFactory>().FromNew().AsSingle();
    }
}