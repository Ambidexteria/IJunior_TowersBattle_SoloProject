using UnityEngine;

namespace Base.Services.AssetManagment
{
    public class AssetLoader : IService
    {
        private const string ErrorMessage = "AssetLoader: cannot load resource";

        public Type Instantiate<Type>(string path) where Type : MonoBehaviour
        {
            Type resource = Resources.Load<Type>(path);

            if (resource == null)
               throw new System.InvalidOperationException($"{nameof(AssetLoader)} - {ErrorMessage}");

            return Object.Instantiate(resource);
        }
    }
}