using Base.UI.Controller;
using Zenject;

public class UIControllerModelInstaller : Installer<UIControllerModelInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromNew().AsSingle();
    }
}