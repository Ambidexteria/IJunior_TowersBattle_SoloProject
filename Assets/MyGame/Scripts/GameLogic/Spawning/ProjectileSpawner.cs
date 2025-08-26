using UnityEngine;

public class ProjectileSpawner : GenericSpawner<Projectile>
{
    private readonly Transform _parentObject;

    public ProjectileSpawner(SpawnerSettings settings, GenericSpawnableObjectFactory<Projectile> factory, Transform parentObject) 
        : base(settings, factory)
    {
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
