using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    private const string FirstScene = nameof(FirstScene);

    [SerializeField] private ButtonClickHandler _startBattleButton;

    [SerializeField] private UIWindowController _settingsWindow;
    [SerializeField] private ButtonClickHandler _showSettingsButton;
    [SerializeField] private ButtonClickHandler _hideSettingsButton;

    [SerializeField] private UIWindowController _stagesWindow;
    [SerializeField] private ButtonClickHandler _showStagesButton;
    [SerializeField] private ButtonClickHandler _hideStagesButton;

    [SerializeField] private UIWindowController _shopWindow;
    [SerializeField] private ButtonClickHandler _showShopButton;
    [SerializeField] private ButtonClickHandler _hideShopButton;

    private void OnEnable()
    {
        _startBattleButton.Clicked += LaunchBattle;

        _showSettingsButton.Clicked += ShowSettingsMenu;
        _hideSettingsButton.Clicked += HideSettingsMenu;

        _showStagesButton.Clicked += ShowStagesMenu;
        _hideStagesButton.Clicked += HideStagesMenu;

        _showShopButton.Clicked += ShowShopMenu;
        _hideShopButton.Clicked += HideShopMenu;
    }

    private void OnDisable()
    {
        _startBattleButton.Clicked -= LaunchBattle;

        _showSettingsButton.Clicked -= ShowSettingsMenu;
        _hideSettingsButton.Clicked -= HideSettingsMenu;

        _showStagesButton.Clicked -= ShowStagesMenu;
        _hideStagesButton.Clicked -= HideStagesMenu;

        _showShopButton.Clicked -= ShowShopMenu;
        _hideShopButton.Clicked -= HideShopMenu;
    }

    private void LaunchBattle()
    {
        SceneManager.LoadScene(FirstScene);
    }

    private void ShowSettingsMenu()
    {
        _settingsWindow.Show();
    }

    private void HideSettingsMenu()
    {
        _settingsWindow.Hide();
    }

    private void ShowStagesMenu()
    {
        _stagesWindow.Show();
    }

    private void HideStagesMenu()
    {
        _stagesWindow.Hide();
    }

    private void ShowShopMenu()
    {
        _shopWindow.Show();
    }

    private void HideShopMenu()
    {
        _shopWindow.Hide();
    }
}
