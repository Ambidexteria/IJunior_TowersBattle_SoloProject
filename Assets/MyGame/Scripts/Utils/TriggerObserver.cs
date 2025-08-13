using System;
using UnityEngine;

namespace Base.Logic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        public event Action<Collider> Entered;
        public event Action<Collider> Exited;

        private void Awake()
        {
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
