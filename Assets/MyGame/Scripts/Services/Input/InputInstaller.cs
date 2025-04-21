using Zenject;
using UnityEngine;

public class InputInstaller : Installer<InputInstaller>
{
    public override void InstallBindings()
    {
        Debug.Log("Input installed");
        DiContainer temp = Container;
        Debug.Log($"is container null ? -- {Container is null}");
        Container.Bind<PlayerInput>().FromNew().AsSingle();
    }
}