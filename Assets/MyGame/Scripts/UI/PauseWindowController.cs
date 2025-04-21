using System;
using UnityEngine;

public class PauseWindowController : UIWindowController
{
    [SerializeField] private TimeController _timeController;
    [SerializeField] private SceneChangeController _sceneChangeController;
    [SerializeField] private ButtonClickHandler _resumeButton;
    [SerializeField] private ButtonClickHandler _settingsButton;
    [SerializeField] private UIWindowController _settingWindowController;

    public event Action Closed;

    private void OnEnable()
    {
        _timeController.Pause();

        _settingsButton.Clicked += OnSettingsButtonClicked;
        _resumeButton.Clicked += OnResumeButtonClicked;
    }

    private void OnDisable()
    {
        _timeController.Resume();

        _settingsButton.Clicked -= OnSettingsButtonClicked;
        _resumeButton.Clicked -= OnResumeButtonClicked;
    }

    private void OnResumeButtonClicked()
    {
        Hide();
        Closed?.Invoke();
    }

    private void OnSettingsButtonClicked()
    {
        _settingWindowController.Show();
    }
}
