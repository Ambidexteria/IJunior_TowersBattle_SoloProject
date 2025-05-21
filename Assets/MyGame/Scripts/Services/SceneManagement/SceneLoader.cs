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
            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(name, onLoaded));
        }

        private IEnumerator LoadSceneCoroutine(string nextScene, Action onLoaded)
        {
            //if (IsSceneAlreadyLoaded(nextScene))
            //{
            //    onLoaded?.Invoke();
            //    yield break;
            //}

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);

            while (asyncOperation.isDone == false)
            {
                yield return null;
            }

            onLoaded?.Invoke();
            yield break;
        }

        private bool IsSceneAlreadyLoaded(string nextScene)
        {
            return SceneManager.GetActiveScene().name == nextScene;
        }
    }
}
