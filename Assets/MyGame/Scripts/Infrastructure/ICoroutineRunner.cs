using System.Collections;
using UnityEngine;

namespace Base.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine LaunchCoroutine(IEnumerator enumerator);
        void EndCoroutine(Coroutine coroutine);
    }
}
