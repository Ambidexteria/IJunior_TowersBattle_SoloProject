using UnityEngine;
using Zenject;

namespace Base.Infrastructure
{
    public class LoadingCurtainInstaller : MonoInstaller<LoadingCurtainInstaller>
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            ExceptionsTest.NullRefTest(nameof(LoadingCurtainInstaller), nameof(InstallBindings), _loadingCurtain);

            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
        }
    }
}