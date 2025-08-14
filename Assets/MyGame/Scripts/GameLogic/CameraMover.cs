using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Base.GameLogic
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform[] _positions;
        [SerializeField] private ButtonClickHandler _changeCameraPositionButton;

        private int _positionIndex = 0;
        private ISaveLoadService _saveLoadService;
        private IPersisentDataService _dataService;

        [Inject]
        private void Init(ISaveLoadService saveLoadService, IPersisentDataService dataService)
        {
            _saveLoadService = saveLoadService;
            _dataService = dataService;
        }

        private void Awake()
        {
            _positionIndex = _dataService.GameData.GameSettings.CameraPosition;
            MoveTo(_positionIndex);
        }

        private void OnEnable()
        {
            _changeCameraPositionButton.Clicked += ChangePositionIndex;
        }

        private void OnDisable()
        {
            _changeCameraPositionButton.Clicked -= ChangePositionIndex;
        }

        private void ChangePositionIndex()
        {
            if (_positionIndex + 1 == _positions.Length)
                _positionIndex = 0;
            else
                _positionIndex += 1;

            _dataService.GameData.GameSettings.CameraPosition = _positionIndex;
            _saveLoadService.SaveProgress();

            MoveTo(_positionIndex);
        }

        private void MoveTo(int positionIndex)
        {
            Transform transform = _positions[positionIndex];

            _camera.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}
