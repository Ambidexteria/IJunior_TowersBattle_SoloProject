using UnityEngine;
using System;

namespace Base.Services.AssetManagment
{
    public class AssetLoader : IService
    {
        private const string ErrorMessage = "AssetLoader: cannot load resource";

        public GameObject Instantiate(string path)
        {
            ExceptionsTest.NullRefMethodTest(nameof(AssetLoader), nameof(Instantiate), path);

            var gameobject = Resources.Load<GameObject>(path);
            return UnityEngine.Object.Instantiate(gameobject);
        }

        public Type Instantiate<Type>(string path) where Type : MonoBehaviour
        {
            ExceptionsTest.NullRefMethodTest(nameof(AssetLoader), nameof(Instantiate), path);

            Type resource = Resources.Load<Type>(path);

            if (resource == null)
                Debug.LogError($"{nameof(AssetLoader)} - {ErrorMessage}");

            return UnityEngine.Object.Instantiate(resource);
        }

        public GameObject InstantiateAt(string path, GameObject initialPoint)
        {
            ExceptionsTest.NullRefMethodTest(nameof(AssetLoader), nameof(InstantiateAt), path, initialPoint);

            var gameobject = Resources.Load<GameObject>(path);
            return UnityEngine.Object.Instantiate(gameobject, initialPoint.transform.position, Quaternion.identity);
        }
    }
}