using UnityEngine;

namespace Base.Services.AssetManagment
{
    public class AssetLoader : IService
    {
        public GameObject Instantiate(string path)
        {
            var gameobject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameobject);
        }

        public GameObject InstantiateAt(string path, GameObject initialPoint)
        {
            var gameobject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameobject, initialPoint.transform.position, Quaternion.identity);
        }
    }
}