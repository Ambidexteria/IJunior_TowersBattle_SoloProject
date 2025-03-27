using UnityEngine;

public class GameSceneUIController : MonoBehaviour
{
    [SerializeField] private UIWindowController _background;

    [SerializeField] private ButtonClickHandler _pauseButton;
    [SerializeField] private PauseWindowController _pauseWindow;
    [SerializeField] private UIWindowController _sceneChangingButtons;

    [SerializeField] private UIWindowController _winMessage;
    [SerializeField] private UIWindowController _defeatMessage;

    private float _defaultTimeScale;

    private void Awake()
    {
        _defaultTimeScale = Time.timeScale;
    }

    private void OnEnable()
    {
        _pauseButton.Clicked += OnPauseButtonClicked;
        _pauseWindow.Closed += OnPauseWindowClose;
    }

    private void OnDisable()
    {
        _pauseButton.Clicked -= OnPauseButtonClicked;
        _pauseWindow.Closed -= OnPauseWindowClose;
    }

    public void ShowWinMessage()
    {
        _winMessage.Show();
        _background.Show();
        _sceneChangingButtons.Show();
    }

    public void ShowDefeatMessage()
    {
        _defeatMessage.Show();
        _background.Show();
        _sceneChangingButtons.Show();
    }

    private void OnPauseButtonClicked()
    {
        _pauseWindow.Show();
        _sceneChangingButtons.Show();
    }

    private void OnPauseWindowClose()
    {
        _sceneChangingButtons.Hide();
    }
}
