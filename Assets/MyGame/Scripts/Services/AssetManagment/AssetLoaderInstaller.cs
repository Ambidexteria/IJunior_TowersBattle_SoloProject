using Base.Services.AssetManagment;
using Zenject;

public class AssetLoaderInstaller : Installer<AssetLoaderInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<AssetLoader>().FromNew().AsSingle();
    }
}