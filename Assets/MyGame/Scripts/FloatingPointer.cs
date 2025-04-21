using UnityEngine;

public class FloatingPointer : MonoBehaviour
{
    [SerializeField] private Transform _pointerObject;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _amplitude = 0.2f;
    [SerializeField] private float _height = 4f;

    private float _currentAngle = 0f;
    private Transform _target;
    private bool _active = false;

    private void Awake()
    {
        _pointerObject.transform.localPosition += new Vector3(0, _height, 0);
    }

    private void Update()
    {
        if (_active)
        {
            Float();

            if (_target != null)
            {
                transform.position = _target.position;
            }
        }
    }

    public void PlaceAbove(Transform target)
    {
        _target = target;
        _active = true;
        _pointerObject.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _active = false;
        _pointerObject.gameObject.SetActive(false);
    }

    private void Float()
    {
        float yOffset;
        float angle = Mathf.PI * Time.deltaTime * _speed;

        _currentAngle += angle;
        yOffset = Mathf.Sin(_currentAngle) * _amplitude;

        Vector3 position = _pointerObject.transform.position;
        position.y += yOffset;
        _pointerObject.transform.position = position;
    }
}
