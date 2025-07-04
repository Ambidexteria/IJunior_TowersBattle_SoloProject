using UnityEngine;

namespace Base.GameLogic
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform[] _positions;
        [SerializeField] private ButtonClickHandler _changeCameraPositionButton;

        private int _positionIndex = 0;

        private void OnEnable()
        {

            _changeCameraPositionButton.Clicked += MoveToNextPosition;
        }

        private void OnDisable()
        {
            _changeCameraPositionButton.Clicked -= MoveToNextPosition;
        }

        private void MoveToNextFirstPosition()
        {
            _camera.transform.SetPositionAndRotation(_positions[0].position, _positions[0].rotation);
        }

        private void MoveToNextPosition()
        {
            if (_positionIndex + 1 == _positions.Length)
                _positionIndex = 0;
            else
                _positionIndex += 1;

            Transform transform = _positions[_positionIndex];

            _camera.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}
