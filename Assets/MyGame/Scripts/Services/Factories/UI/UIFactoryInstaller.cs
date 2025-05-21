using Base.Services.Factories.UI;
using UnityEngine;
using Zenject;

public class UIFactoryInstaller : Installer<UIFactoryInstaller>
{
    public override void InstallBindings()
    {
        //Container.Bind<IUIFactory>().To<GameSceneUIFactory>().AsSingle();
    }
}