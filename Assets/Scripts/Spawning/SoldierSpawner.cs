using Zenject;

public class SoldierSpawner : GenericSpawner<Soldier>
{
    [Inject]
    public SoldierSpawner(SoldierForDespawnDetector despawner, SpawnerSettings settings, GenericSpawnableObjectFactory<Soldier> factory) : base(settings, factory) 
    {
    }
}
