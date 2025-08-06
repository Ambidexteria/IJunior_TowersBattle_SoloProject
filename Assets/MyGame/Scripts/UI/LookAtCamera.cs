using UnityEngine;

namespace Base
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool _lookAlongCameraView = false;

        private Camera _main;

        private void Awake()
        {
            _main = Camera.main;
            Look();
        }

        private void Update()
        {
            Look();
        }

        private void Look()
        {
            Quaternion rotation = _main.transform.rotation;
            Vector3 direction = Vector3.back;

            if (_lookAlongCameraView)
                direction = Vector3.forward;

            transform.LookAt(transform.position + rotation * direction, rotation * Vector3.up);
        }
    }
}
