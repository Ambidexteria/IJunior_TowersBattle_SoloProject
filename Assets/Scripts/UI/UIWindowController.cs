using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIWindowController : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
