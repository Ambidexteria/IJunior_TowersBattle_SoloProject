using Base.GameLogic.Cannon;
using Base.Soldier;
using UnityEngine;
using Zenject;

public class FactoriesMonoInstaller : MonoInstaller
{
    [SerializeField] private Transform _projectilesParent;
    [SerializeField] private SoldierSetup _soldierPrefab;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private CannonProjectile _cannonProjectilePrefab;
    [SerializeField] private SpawnerSettings _projectileSpawnerSettings;
    [SerializeField] private SpawnerSettings _cannonProjectileSpawnerSettings;

    public override void InstallBindings()
    {
        Container.Bind<SoldierForDespawnDetector>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindFactory<SoldierSetup, GenericSpawnableObjectFactory<SoldierSetup>>().FromComponentInNewPrefab(_soldierPrefab).NonLazy();

        Container.Bind<ProjectileSpawner>().AsSingle().WithArguments(_projectileSpawnerSettings, _projectilesParent).NonLazy();
        Container.BindFactory<Projectile, GenericSpawnableObjectFactory<Projectile>>().FromComponentInNewPrefab(_projectilePrefab).NonLazy();

        Container.Bind<CannonProjectileSpawner>().AsSingle().WithArguments(_cannonProjectileSpawnerSettings).NonLazy();
        Container.BindFactory<CannonProjectile, GenericSpawnableObjectFactory<CannonProjectile>>().FromComponentInNewPrefab(_cannonProjectilePrefab).NonLazy();
    }
}