using UnityEngine;

namespace Base.Services.AssetManagment
{
    public class AssetLoader : IService
    {
        private const string ErrorMessage = "AssetLoader: cannot load resource";

        public T Instantiate<T>(string path) 
            where T : MonoBehaviour
        {
            T resource = Resources.Load<T>(path);

            if (resource == null)
               throw new System.InvalidOperationException($"{nameof(AssetLoader)} - {ErrorMessage}");

            return Object.Instantiate(resource);
        }
    }
}