using UnityEngine;
using Zenject;

public class FactoriesMonoInstaller : MonoInstaller
{
    [SerializeField] private Soldier _soldier;

    public override void InstallBindings()
    {
        Container.BindFactory<Soldier, GenericSpawnableObjectFactory<Soldier>>().FromComponentInNewPrefab(_soldier).AsSingle().NonLazy();
    }
}