using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    private const string MainMenuScene = nameof(MainMenuScene);
    private const string GameScene = nameof(GameScene);

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
        SceneManager.LoadScene(MainMenuScene);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
