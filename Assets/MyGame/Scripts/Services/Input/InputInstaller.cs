using Zenject;
using UnityEngine;

namespace Base.Services.Input
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<InputService>().FromNew().AsSingle();
        }
    }
}
