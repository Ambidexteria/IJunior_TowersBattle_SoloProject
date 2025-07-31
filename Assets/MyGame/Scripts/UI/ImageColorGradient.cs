using UnityEngine;
using UnityEngine.UI;

public class ImageColorGradient : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Gradient _gradient;

    private void Update()
    {
        _image.color = _gradient.Evaluate(_image.fillAmount);
    }
}
