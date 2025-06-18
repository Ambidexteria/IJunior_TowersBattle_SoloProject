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
            ExceptionsTest.NullRefConstructorTest(nameof(SceneLoader), coroutineRunner);

            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string name, Action onLoaded = null)
        {
            ExceptionsTest.NullRefMethodTest(nameof(SceneLoader), nameof(LoadScene), name, onLoaded);

            _coroutineRunner.LaunchCoroutine(LoadSceneCoroutine(name, onLoaded));
        }

        private IEnumerator LoadSceneCoroutine(string nextScene, Action onLoaded)
        {
            if (IsSceneAlreadyLoaded(nextScene))
            {
                AsyncOperation loadEmptyScene = SceneManager.LoadSceneAsync(SceneNames.EmptyScene);

                while (loadEmptyScene.isDone == false)
                    yield return null;
            }

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
