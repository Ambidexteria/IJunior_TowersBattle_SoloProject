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
        }

        private void Update()
        {
            Quaternion rotation = _main.transform.rotation;
            Vector3 direction = Vector3.back;

            if(_lookAlongCameraView)
                direction = Vector3.forward;

            transform.LookAt(transform.position + rotation * direction, rotation * Vector3.up);
        }
    }
}
