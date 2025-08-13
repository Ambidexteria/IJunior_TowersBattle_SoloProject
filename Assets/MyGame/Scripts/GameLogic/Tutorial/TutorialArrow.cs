using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityExtensions;

public class TutorialArrow : MonoBehaviour
{
    [SerializeField] private ImageResizer _imageResizer;
    [SerializeField] private Image _arrow;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _verticalOffset = 50f;
    [SerializeField] private float _movementHeight = 50f;

    private Sequence _sequence;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    public void PlaceAbove(Transform target)
    {
        _imageResizer.Resize(_arrow);

        _startPosition = Camera.main.WorldToScreenPoint(target.position).AddY(_verticalOffset);
        _endPosition = _startPosition.AddY(_movementHeight);

        _arrow.rectTransform.anchoredPosition = _startPosition;
        _arrow.enabled = true;

        Move();
    }

    public void Hide()
    {
        if (_arrow != null)
            _arrow.enabled = false;

        _sequence.Pause();
    }

    private void Move()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(_arrow.rectTransform.DOAnchorPosY(_endPosition.y, _moveTime))
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        DOTween.Play(_sequence);
    }
}
