using Base.GameLogic.Cannon;

public class CannonProjectileSpawner : GenericSpawner<CannonProjectile>
{
    public CannonProjectileSpawner(SpawnerSettings settings, GenericSpawnableObjectFactory<CannonProjectile> factory) : base(settings, factory)
    {
    }

    protected override void PrepareForSpawn(ref CannonProjectile projectile)
    {
        projectile.Despawning += Despawn;
    }

    protected override void PrepareForDespawn(ref CannonProjectile projectile)
    {
        projectile.Despawning -= Despawn;
    }
}
