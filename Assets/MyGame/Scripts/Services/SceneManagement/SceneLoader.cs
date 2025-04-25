using Base.Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Services.SceneManagment
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) 
        { 
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string name, Action onLoaded = null)
        {
            Debug.Log("Loading scene...");
            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(name, onLoaded));
        }

        private IEnumerator LoadSceneCoroutine(string nextScene, Action onLoaded)
        {
            Debug.Log("Coroutine running...");
            if (IsSceneAlreadyLoaded(nextScene))
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);

            while (asyncOperation.isDone == false)
            {
                Debug.Log("Coroutine still running");
                yield return null;
            }

            Debug.Log("Coroutine ended");
            onLoaded?.Invoke();
            yield break;
        }

        private bool IsSceneAlreadyLoaded(string nextScene)
        {
            return SceneManager.GetActiveScene().name == nextScene;
        }
    }
}
