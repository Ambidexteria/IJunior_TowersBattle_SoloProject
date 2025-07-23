using UnityEngine;

public class CameraSize : MonoBehaviour
{
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _width = _camera.pixelWidth;
        _height = _camera.pixelHeight;
    }
}
