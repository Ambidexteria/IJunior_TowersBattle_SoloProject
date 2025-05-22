using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine LaunchCoroutine(IEnumerator enumerator);
        void EndCoroutine(Coroutine coroutine);
    }
}
