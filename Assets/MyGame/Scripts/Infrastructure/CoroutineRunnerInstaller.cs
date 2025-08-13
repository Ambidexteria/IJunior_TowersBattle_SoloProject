using Base.Infrastructure;
using UnityEngine;
using Zenject;

namespace Base
{
    public class CoroutineRunnerInstaller : MonoInstaller<CoroutineRunnerInstaller>
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
        }
    }
}
