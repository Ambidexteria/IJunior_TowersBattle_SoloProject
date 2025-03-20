using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MinigamePressRange : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _pressRange = 0.1f;
    [SerializeField] private Image _image;

    private RectTransform _rectTransform;
    private float _minPressValue;
    private float _maxPressValue;
    private float _pressRangeWidth;

    public RectTransform PressRangeImageTransform => _image.rectTransform;
    public float FullRangeMinValue => _rectTransform.anchoredPosition.x;
    public float FullRangeMaxValue => _rectTransform.rect.width;
    public float MaxPressValue => _maxPressValue;
    public float MinPressValue => _minPressValue;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Place()
    {
        CalculateValues();

        PressRangeImageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _pressRangeWidth);
        SetPositionX(_minPressValue);
    }

    private void SetPositionX(float x)
    {
        Vector2 position = PressRangeImageTransform.anchoredPosition;
        position.x = x;
        PressRangeImageTransform.anchoredPosition = position;
    }

    private void CalculateValues()
    {
        _pressRangeWidth = _rectTransform.rect.width * _pressRange;

        _minPressValue = Random.Range(0, _rectTransform.rect.width - _pressRangeWidth);
        _maxPressValue = _minPressValue + _pressRangeWidth;
    }
}
