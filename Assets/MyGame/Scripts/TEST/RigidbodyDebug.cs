using UnityEngine;

namespace Base.Test.Unity
{
    [RequireComponent (typeof (Rigidbody))]
    public class RigidbodyDebug : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody> ();

            ExceptionsTest.NullRefMethodTest(nameof(RigidbodyDebug), nameof(Awake), _rigidbody);
        }

        [ContextMenu(nameof(ShowVecloity))]
        public void ShowVecloity()
        {
            Debug.Log($"Root: {transform.root.name}; gameobject: {transform.name}\nRigidbody velocity = {_rigidbody.velocity}");
        }
        
        [ContextMenu(nameof(ShowIsInSleepState))]
        public void ShowIsInSleepState()
        {
            Debug.Log($"Root: {transform.root.name}; gameobject: {transform.name}\nRigidbody isSleeping = {_rigidbody.IsSleeping()}");
        }
    }
}
