using UnityEngine;
using Zenject;

public class FactoriesMonoInstaller : MonoInstaller
{
    [SerializeField] private Soldier _soldierPrefab;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private CannonProjectile _cannonProjectilePrefab;
    [SerializeField] private SpawnerSettings _soldierSpawnerSettings;
    [SerializeField] private SpawnerSettings _projectileSpawnerSettings;
    [SerializeField] private SpawnerSettings _cannonProjectileSpawnerSettings;


    public override void InstallBindings()
    {
        Container.Bind<SoldierForDespawnDetector>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SoldierSpawner>().AsSingle().WithArguments(_soldierSpawnerSettings).NonLazy();
        Container.BindFactory<Soldier, GenericSpawnableObjectFactory<Soldier>>().FromComponentInNewPrefab(_soldierPrefab).NonLazy();

        Container.Bind<ProjectileSpawner>().AsSingle().WithArguments(_projectileSpawnerSettings).NonLazy();
        Container.BindFactory<Projectile, GenericSpawnableObjectFactory<Projectile>>().FromComponentInNewPrefab(_projectilePrefab).NonLazy();

        Container.Bind<CannonProjectileSpawner>().AsSingle().WithArguments(_cannonProjectileSpawnerSettings).NonLazy();
        Container.BindFactory<CannonProjectile, GenericSpawnableObjectFactory<CannonProjectile>>().FromComponentInNewPrefab(_cannonProjectilePrefab).NonLazy();
    }
}