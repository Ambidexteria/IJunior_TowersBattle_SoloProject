using Zenject;

public class GenericSpawnableObjectFactory<T> : PlaceholderFactory<T> 
    where T : SpawnableObject
{
}
