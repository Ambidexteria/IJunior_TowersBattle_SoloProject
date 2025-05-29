using UnityEngine;
using Zenject;

namespace Base.Infrastructure
{
    public class LoadingCurtainInstaller : MonoInstaller<LoadingCurtainInstaller>
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
        }
    }
}