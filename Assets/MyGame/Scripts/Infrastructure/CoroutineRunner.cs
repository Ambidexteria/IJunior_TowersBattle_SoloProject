using Base.Data.Scenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Infrastructure
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private List<Coroutine> _launchedCoroutines = new();

        public Coroutine LaunchCoroutine(IEnumerator enumerator)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CoroutineRunner), nameof(EndCoroutine), enumerator);

            var coroutine = StartCoroutine(enumerator);
            _launchedCoroutines.Add(coroutine);

            return coroutine;
        }

        public void EndCoroutine(Coroutine coroutine)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CoroutineRunner), nameof(EndCoroutine), coroutine);

            _launchedCoroutines?.Remove(coroutine);

            StopCoroutine(coroutine);
        }

        //[ContextMenu(nameof(ShowLaunchedCoroutine))]
        //private void ShowLaunchedCoroutine()
        //{
        //    string text = $"{nameof(ICoroutineRunner)} - launched coroutines ({_launchedCoroutines.Count}):\n\n";

        //    foreach (var item in _launchedCoroutines)
        //    {
        //        text += item.ToString() + "\n";
        //    }

        //    Debug.Log(text);
        //}
    }
}
