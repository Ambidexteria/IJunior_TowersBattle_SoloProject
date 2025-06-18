using System;
using UnityEngine;

namespace Base.Logic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        private Collider _collider;

        public event Action<Collider> Entered;
        public event Action<Collider> Exited;

        private void Awake()
        {
            _collider = GetComponent<Collider>();

            ExceptionsTest.NullRefMethodTest(nameof(TriggerObserver), nameof(Awake), _collider);

            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            Entered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Exited?.Invoke(other);
        }
    }
}
