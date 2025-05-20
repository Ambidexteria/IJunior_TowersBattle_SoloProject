using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base
{
    public class SceneLoaderTest : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _button;
        [SerializeField] private string _sceneName;

        private void OnEnable()
        {
            _button.Clicked += ChangeScene;
        }

        private void OnDisable()
        {
            _button.Clicked -= ChangeScene;
        }

        private void ChangeScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
