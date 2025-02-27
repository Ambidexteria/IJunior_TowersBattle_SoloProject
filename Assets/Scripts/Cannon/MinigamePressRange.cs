using UnityEngine;
using UnityEngine.UI;

public class MinigamePressRange : MonoBehaviour
{
    [SerializeField] private Image _image;

    public RectTransform PressRangeImageTransform => _image.rectTransform;

    private void Awake()
    {
        SetPositionX(25);
        SetWidth(45);
    }

    public void SetPositionX(float x)
    {
        Vector2 position = PressRangeImageTransform.anchoredPosition;
        position.x = x;
        PressRangeImageTransform.anchoredPosition = position;
    }

    public void SetWidth(float width)
    {
        Rect rect = PressRangeImageTransform.rect;
        rect.width = width;
        PressRangeImageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
