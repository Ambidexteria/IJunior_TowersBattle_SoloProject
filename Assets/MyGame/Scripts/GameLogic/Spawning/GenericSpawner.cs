using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public abstract class GenericSpawner<Type> where Type
    : SpawnableObject
{
    private readonly int _poolDefaultCapacity = 20;
    private readonly int _poolMaxSize = 100;

    private ObjectPool<Type> _pool;
    private GenericSpawnableObjectFactory<Type> _factory;

    private int _objectNumber;

    [Inject]
    public GenericSpawner(SpawnerSettings settings, GenericSpawnableObjectFactory<Type> factory)
    {
        _poolDefaultCapacity = settings.PoolDefaultCapacity;
        _poolMaxSize = settings.PoolMaxSize;
        _factory = factory;

        InitializePool();
        PrepareOnAwake();
    }

    public Type Spawn()
    {
        return _pool.Get();
    }

    public void Despawn(Type spawnableObject)
    {
        PrepareForDespawn(ref spawnableObject);
        _pool.Release(spawnableObject);
    }

    protected virtual void PrepareOnAwake() { }

    protected virtual void PrepareForSpawn(ref Type spawnableObject) { }

    protected virtual void PrepareForDespawn(ref Type spawnableObject) { }

    protected virtual void PrepareOnCreateObject(ref Type spawnableObject) { }

    private void InitializePool()
    {
        _pool = new ObjectPool<Type>(
            createFunc: () => Create(),
            actionOnGet: (spawnableObject) => PrepareForSpawn(ref spawnableObject),
            actionOnRelease: (spawnableObject) => spawnableObject.gameObject.SetActive(false),
            actionOnDestroy: (spawnableObject) => GameObject.Destroy(spawnableObject.gameObject),
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxSize);
    }

    private Type Create()
    {
        Type type = _factory.Create();
        _objectNumber++;
        type.gameObject.name += "_" + _objectNumber.ToString();
        PrepareOnCreateObject(ref type);

        return type;
    }
}