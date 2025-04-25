using Base;
using Zenject;

public class TestInstaller : Installer<TestInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<TestScript>().AsSingle();
    }
}