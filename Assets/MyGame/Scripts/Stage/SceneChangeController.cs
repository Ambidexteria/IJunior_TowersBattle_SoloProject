using Base.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    [SerializeField] private ButtonClickHandler _mainMenuButton;
    [SerializeField] private ButtonClickHandler _startNewBattleButton;

    private void OnEnable()
    {
        _mainMenuButton.Clicked += LoadMainMenu;
        _startNewBattleButton.Clicked += LoadGameScene;
    }

    private void OnDisable()
    {
        _mainMenuButton.Clicked -= LoadMainMenu;
        _startNewBattleButton.Clicked -= LoadGameScene;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(SceneNames.Game);
    }

    public void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
