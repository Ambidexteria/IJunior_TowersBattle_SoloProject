using UnityEngine;

public class SceneChangingButtonsController : UIWindowController
{
    [SerializeField] private ButtonClickHandler _retryButton;
    [SerializeField] private ButtonClickHandler _homeButton;

    private void OnEnable()
    {
        _retryButton.Clicked += OnRetryButtonClicked;
        _homeButton.Clicked += OnHomeButtonClicked;
    }

    private void OnDisable()
    {
        _retryButton.Clicked -= OnRetryButtonClicked;
        _homeButton.Clicked -= OnHomeButtonClicked;
    }
    private void OnRetryButtonClicked()
    {
    }

    private void OnHomeButtonClicked()
    {
    }
}
