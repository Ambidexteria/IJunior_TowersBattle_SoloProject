using Base.Data;
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

        public event Action<string> LoadingScene;

        public void LoadScene(string name, Action onLoaded = null)
        {
            Debug.Log("LOADING SCENE");
            LoadingScene?.Invoke(name);
            _coroutineRunner.LaunchCoroutine(LoadSceneCoroutine(name, onLoaded));
        }

        private IEnumerator LoadSceneCoroutine(string nextScene, Action onLoaded)
        {
            if (IsSceneAlreadyLoaded(nextScene))
            {
                Debug.LogWarning($"{nameof(SceneLoader)} - loading {SceneNames.EmptyScene} instead of {nextScene}");

                AsyncOperation loadEmptyScene = SceneManager.LoadSceneAsync(SceneNames.EmptyScene);

                while (loadEmptyScene.isDone == false)
                    yield return null;

                //onLoaded?.Invoke();
                //yield break;
            }

            Debug.LogWarning($"{nameof(SceneLoader)} - loading {nextScene}");

            AsyncOperation loadNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (loadNextScene.isDone == false)
                yield return null;

            onLoaded?.Invoke();
            yield break;
        }

        private bool IsSceneAlreadyLoaded(string nextScene)
        {
            return SceneManager.GetActiveScene().name == nextScene;
        }
    }
}
