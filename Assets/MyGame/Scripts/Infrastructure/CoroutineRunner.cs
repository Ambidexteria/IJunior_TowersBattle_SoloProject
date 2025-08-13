using Base.Data.Scenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Infrastructure
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private readonly List<Coroutine> _launchedCoroutines = new();

        public Coroutine LaunchCoroutine(IEnumerator enumerator)
        {
            var coroutine = StartCoroutine(enumerator);
            _launchedCoroutines.Add(coroutine);

            return coroutine;
        }

        public void EndCoroutine(Coroutine coroutine)
        {
            _launchedCoroutines?.Remove(coroutine);

            StopCoroutine(coroutine);
        }
    }
}
