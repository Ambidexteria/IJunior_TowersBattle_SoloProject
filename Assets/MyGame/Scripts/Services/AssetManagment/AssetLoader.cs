using System.Security.Cryptography;
using UnityEngine;
using Zenject;

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
                Debug.LogError($"{nameof(InstantiateMainMenuUI)} - {ErrorMessage}");

            return Object.Instantiate(resource);
        }

        public MainMenuUISetup InstantiateMainMenuUI(string path)
        {
            MainMenuUISetup mainmenu = Resources.Load<MainMenuUISetup>(path);

            if (mainmenu == null)
                Debug.LogError($"{nameof(InstantiateMainMenuUI)} - {ErrorMessage}");

            return Object.Instantiate(mainmenu);
        }

        public GameObject InstantiateAt(string path, GameObject initialPoint)
        {
            var gameobject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameobject, initialPoint.transform.position, Quaternion.identity);
        }
    }
}