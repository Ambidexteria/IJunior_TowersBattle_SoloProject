using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class UIWindowController : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _canvasGroup.interactable = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _canvasGroup.interactable = false;
        gameObject.SetActive(false);
    }
}
