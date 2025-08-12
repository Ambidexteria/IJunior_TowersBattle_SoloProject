using UnityEngine;

public class ProjectileSpawner : GenericSpawner<Projectile>
{
    private Transform _parentObject;

    public ProjectileSpawner(SpawnerSettings settings, GenericSpawnableObjectFactory<Projectile> factory, Transform parentObject) : base(settings, factory)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(ProjectileSpawner), settings, factory, parentObject);

        _parentObject = parentObject;
    }

    protected override void PrepareForSpawn(ref Projectile projectile)
    {
        projectile.Despawning += Despawn;
    }

    protected override void PrepareForDespawn(ref Projectile projectile)
    {
        projectile.Despawning -= Despawn;
    }

    protected override void PrepareOnCreateObject(ref Projectile projectile)
    {
        projectile.transform.SetParent(_parentObject);
    }
}
