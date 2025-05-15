using UnityEngine;

namespace Base.Services.AssetManagment
{
    public class AssetLoader : IService
    {
        private const string ErrorMessage = "cannot load resource";

        public GameObject Instantiate(string path)
        {
            var gameobject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameobject);
        }

        public Type Instantiate<Type>(string path) where Type : MonoBehaviour
        {
            Type resource = Resources.Load<Type>(path);

            if (resource == null)
                Debug.LogError($"{nameof(AssetLoader)} - {ErrorMessage}");

            return Object.Instantiate(resource);
        }

        public GameObject InstantiateAt(string path, GameObject initialPoint)
        {
            var gameobject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameobject, initialPoint.transform.position, Quaternion.identity);
        }
    }
}