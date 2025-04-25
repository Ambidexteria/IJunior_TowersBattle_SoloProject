using Base.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private ButtonClickHandler _startBattleButton;

    [SerializeField] private UIWindowController _settingsWindow;
    [SerializeField] private ToggleValueChanger _settingsWindowToggle;

    [SerializeField] private UIWindowController _stagesWindow;
    [SerializeField] private ButtonClickHandler _showStagesButton;
    [SerializeField] private ButtonClickHandler _hideStagesButton;

    [SerializeField] private UIWindowController _shopWindow;
    [SerializeField] private ButtonClickHandler _showShopButton;
    [SerializeField] private ButtonClickHandler _hideShopButton;

    private void OnEnable()
    {
        _startBattleButton.Clicked += LaunchBattle;

        _settingsWindowToggle.ValueChanged += OnSettingsTogglePressed;

        _showStagesButton.Clicked += ShowStagesMenu;
        _hideStagesButton.Clicked += HideStagesMenu;

        _showShopButton.Clicked += ShowShopMenu;
        _hideShopButton.Clicked += HideShopMenu;
    }

    private void OnDisable()
    {
        _startBattleButton.Clicked -= LaunchBattle;

        _settingsWindowToggle.ValueChanged -= OnSettingsTogglePressed;

        _showStagesButton.Clicked -= ShowStagesMenu;
        _hideStagesButton.Clicked -= HideStagesMenu;

        _showShopButton.Clicked -= ShowShopMenu;
        _hideShopButton.Clicked -= HideShopMenu;
    }

    private void LaunchBattle()
    {
        SceneManager.LoadScene(SceneNames.Game);
    }

    private void OnSettingsTogglePressed(bool enable)
    {
        _settingsWindow.gameObject.SetActive(enable);
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
