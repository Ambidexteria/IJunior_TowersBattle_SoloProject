using UnityEngine;
using Zenject;

namespace Base
{
    public class TestZenjectInjection : MonoBehaviour
    {
        private TestScript _assetLoader;

        [Inject]
        private void Init(TestScript assetLoader)
        {
            Debug.Log("test script injected");
            _assetLoader = assetLoader;
        }
    }
}
