using UnityEngine;
using Zenject;

namespace Base.Infrastructure
{
    public class EntryPointCreator : MonoBehaviour
    {
        [SerializeField] private EntryPoint _prefab;

        private EntryPoint _entryPoint;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(EntryPointCreator), nameof(Awake),  _prefab);
            _entryPoint = FindFirstObjectByType<EntryPoint>();

            if (_entryPoint == null)
                ProjectContext.Instance.Container.InstantiatePrefab(_prefab);
        }
    }
}
