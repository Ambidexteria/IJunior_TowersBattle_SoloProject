using DG.Tweening;
using UnityEngine;

namespace Base.Utils
{
    public class ScalingUIFrame : MonoBehaviour
    {
        [SerializeField] private RectTransform _frame;
        [SerializeField] private float _scalingTime = 2f;
        [SerializeField] private float _maxScale = 1.5f;

        private RectTransform _target;
        private Sequence _sequence;

        private float _width;
        private float _height;

        public void PlaceAbove(Transform target)
        {
            _target = target.GetComponent<RectTransform>();

            if (target == null)
            {
                Debug.LogError($"target doesn't have RectTransform");
                return;
            }

            _width = _target.rect.width;
            _height = _target.rect.height;

            _frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _width);
            _frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
            _frame.anchoredPosition = new Vector2(_target.anchoredPosition.x, _target.anchoredPosition.y);

            _frame.gameObject.SetActive(true);

            ResizeByDOTween();
        }

        public void Hide()
        {
            _frame.gameObject.SetActive(false);
        }

        private void ResizeByDOTween()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(_frame.DOScale(_maxScale, _scalingTime)).SetEase(Ease.InOutSine);
            _sequence.SetLoops(-1, LoopType.Yoyo);

            DOTween.Play(_sequence);
        }
    }
}
