using Base.Infrastructure;
using Zenject;

namespace Base
{
    public class CoroutineRunnerInstaller : MonoInstaller<CoroutineRunnerInstaller>, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
        }
    }
}
