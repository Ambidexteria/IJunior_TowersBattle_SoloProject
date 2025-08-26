using UnityEngine;
using UnityEngine.UI;

public class ImageResizer : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _scaleFactor;

    public void Resize(Image image)
    {
        float scale;

        float screenWidth = _mainCamera.pixelWidth;
        float screenHeight = _mainCamera.pixelHeight;

        float width = screenWidth * _scaleFactor;
        float height = screenHeight * _scaleFactor;

        if (screenHeight > screenWidth)
            scale = height;
        else
            scale = width;

        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scale);
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scale);
    }
}
