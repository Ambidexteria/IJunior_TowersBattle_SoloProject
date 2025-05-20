using System;
using UnityEngine;

namespace Base
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _resumeButton;
        [SerializeField] private ButtonClickHandler _restartLevelButton;
        [SerializeField] private ButtonClickHandler _returnToMainMenuButton;
        [SerializeField] private ButtonClickHandler _settingsButton;

        public event Action ResumeButtonClicked;
        public event Action RestartLevelButtonClicked;
        public event Action ReturnToMainMenuButtonClicked;
        public event Action SettingsButtonClicked;

        private void OnEnable()
        {
            _resumeButton.Clicked += ResumeButtonClicked;
            _restartLevelButton.Clicked += RestartLevelButtonClicked;
            _returnToMainMenuButton.Clicked += ReturnToMainMenuButtonClicked;
            _settingsButton.Clicked += SettingsButtonClicked;
        }

        private void OnDisable()
        {
            _resumeButton.Clicked -= ResumeButtonClicked;
            _restartLevelButton.Clicked -= RestartLevelButtonClicked;
            _returnToMainMenuButton.Clicked -= ReturnToMainMenuButtonClicked;
            _settingsButton.Clicked -= SettingsButtonClicked;
        }
    }
}
