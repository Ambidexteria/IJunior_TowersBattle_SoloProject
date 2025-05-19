using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    public class ShootMinigamePressRangeView : MonoBehaviour
    {
        [SerializeField] private SliderValueChanger _sliderValueChanger;
        [SerializeField] private Image _pressRangeImage;

        public Vector2 PressRangePosition => _pressRangeImage.rectTransform.anchoredPosition;

        public void SetWidth(float x)
        {
            _pressRangeImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
        }

        public void SetSliderValue(float value)
        {
            _sliderValueChanger.SetValue(value);
        }

        public void SetMinMaxValues(float min, float max)
        {
            Debug.Log("");
            _sliderValueChanger.SetMinMaxValues(min, max);
        }

        public void PlacePressRange(Vector2 position)
        {
            _pressRangeImage.rectTransform.anchoredPosition = position;
        }
    }
}
