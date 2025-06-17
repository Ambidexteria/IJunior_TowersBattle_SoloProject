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
            ExceptionsTest.NullRefTest(nameof(CoroutineRunnerInstaller), nameof(InstallBindings), _coroutineRunner);

            Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
        }
    }
}
