using UnityEngine;

namespace Base
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _main;

        private void Awake()
        {
            _main = Camera.main;
        }

        private void Update()
        {
            Quaternion rotation = _main.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}
