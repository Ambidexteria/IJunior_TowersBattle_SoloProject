using UnityEngine;
using Zenject;

public class ControlsInstaller : Installer<ControlsInstaller>
{
    public override void InstallBindings()
    {
        Debug.Log("Installed");
    }
}