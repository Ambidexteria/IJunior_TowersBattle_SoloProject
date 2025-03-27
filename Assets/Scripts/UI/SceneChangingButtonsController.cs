using UnityEngine;

public class SceneChangingButtonsController : UIWindowController
{
    [SerializeField] private SceneChangeController _sceneChangeController;
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
        _sceneChangeController.LoadGameScene();
    }

    private void OnHomeButtonClicked()
    {
        _sceneChangeController.LoadMainMenu();
    }
}
