using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base
{
    public class MesageOnSceneLoaded : MonoBehaviour
    {
        private void Awake()
        {

        }

        private string GetActiveSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}
