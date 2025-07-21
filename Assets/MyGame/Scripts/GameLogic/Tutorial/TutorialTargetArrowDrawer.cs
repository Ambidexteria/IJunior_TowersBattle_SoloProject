using UnityEngine;
using UnityEngine.UI;
using UnityExtensions;

public class TutorialTargetArrowDrawer : MonoBehaviour
{
    [SerializeField] private Image _arrow;
    [SerializeField] private float _speed;
    [SerializeField] private float _verticalOffset = 50f;
    [SerializeField] private float _movementHeight = 50f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _currentSpeed;

    private void Update()
    {
        if (_arrow.enabled)
            Move();
    }

    public void DrawAbove(Transform target)
    {
        _startPosition = Camera.main.WorldToScreenPoint(target.position).AddY(_verticalOffset);
        _endPosition = _startPosition.AddY(_movementHeight);

        _currentSpeed = _speed;
        _arrow.rectTransform.anchoredPosition = _startPosition;
        _arrow.enabled = true;
    }

    public void HideArrow()
    {
        _arrow.enabled = false;
    }

    private void Move()
    {
        float nextValue = _arrow.rectTransform.anchoredPosition.y;

        nextValue = Mathf.Clamp(nextValue, _startPosition.y, _endPosition.y);
        nextValue = nextValue + _currentSpeed * Time.deltaTime;

        if (IsOutsideFullRange(nextValue))
        {
            _currentSpeed *= -1;
        }

        _arrow.rectTransform.anchoredPosition = new Vector2(_arrow.rectTransform.anchoredPosition.x, nextValue);
    }

    private bool IsOutsideFullRange(float nextValue)
    {
        return nextValue >= _endPosition.y || nextValue <= _startPosition.y;
    }
}
