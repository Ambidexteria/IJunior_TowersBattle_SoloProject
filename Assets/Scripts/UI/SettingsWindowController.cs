using UnityEngine;

public class SettingsWindowController : UIWindowController
{
    [SerializeField] private ButtonClickHandler _closeButton;

    private void OnEnable()
    {
        _closeButton.Clicked += OnCloseButtonClicked;
    }

    private void OnDisable()
    {
        _closeButton.Clicked -= OnCloseButtonClicked;
    }

    private void OnCloseButtonClicked()
    {
        Hide();
    }
}
