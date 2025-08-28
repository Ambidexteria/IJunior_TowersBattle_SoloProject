using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public abstract class GenericSpawner<T>
    where T : SpawnableObject
{
    private readonly int _poolDefaultCapacity = 20;
    private readonly int _poolMaxSize = 100;

    private ObjectPool<T> _pool;
    private GenericSpawnableObjectFactory<T> _factory;

    private int _objectNumber;

    [Inject]
    public GenericSpawner(SpawnerSettings settings, GenericSpawnableObjectFactory<T> factory)
    {
        _poolDefaultCapacity = settings.PoolDefaultCapacity;
        _poolMaxSize = settings.PoolMaxSize;
        _factory = factory;

        InitializePool();
        PrepareOnAwake();
    }

    public T Spawn()
    {
        return _pool.Get();
    }

    public void Despawn(T spawnableObject)
    {
        PrepareForDespawn(ref spawnableObject);
        _pool.Release(spawnableObject);
    }

    protected virtual void PrepareOnAwake() { }

    protected virtual void PrepareForSpawn(ref T spawnableObject) { }

    protected virtual void PrepareForDespawn(ref T spawnableObject) { }

    protected virtual void PrepareOnCreateObject(ref T spawnableObject) { }

    private void InitializePool()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Create(),
            actionOnGet: (spawnableObject) => PrepareForSpawn(ref spawnableObject),
            actionOnRelease: (spawnableObject) => spawnableObject.gameObject.SetActive(false),
            actionOnDestroy: (spawnableObject) => GameObject.Destroy(spawnableObject.gameObject),
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxSize);
    }

    private T Create()
    {
        T type = _factory.Create();
        _objectNumber++;
        type.gameObject.name += "_" + _objectNumber.ToString();
        PrepareOnCreateObject(ref type);

        return type;
    }
}