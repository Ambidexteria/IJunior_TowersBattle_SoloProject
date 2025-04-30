using Base.Services.SaveLoad;
using UnityEngine;
using Zenject;

public class SaveLoadServiceInstaller : Installer<SaveLoadServiceInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ISaveLoadService>().To<SaveLoadService>().FromNew().AsSingle();
    }
}