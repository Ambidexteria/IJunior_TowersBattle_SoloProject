using Base.Services.SaveLoad;
using Zenject;

public class SaveLoadServiceInstaller : Installer<SaveLoadServiceInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ISaveLoadService>().FromInstance(new SaveLoadService()).AsSingle();
    }
}