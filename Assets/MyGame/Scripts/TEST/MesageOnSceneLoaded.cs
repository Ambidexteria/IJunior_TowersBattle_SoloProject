using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base
{
    public class MesageOnSceneLoaded : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log($"{GetActiveSceneName()} succsessfully loaded");

        }

        private string GetActiveSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}
