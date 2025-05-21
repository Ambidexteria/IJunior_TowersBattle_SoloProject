using System;
using UnityEngine;

namespace Base
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _pauseButton;
        [SerializeField] private ButtonClickHandler _restartLevelButton;
        [SerializeField] private ButtonClickHandler _returnToMainMenuButton;
        [SerializeField] private ButtonClickHandler _resumeButton;

        public event Action ResumeButtonClicked;
        public event Action PauseButtonClicked;
        public event Action RestartLevelButtonClicked;
        public event Action ReturnToMainMenuButtonClicked;

        private void OnEnable()
        {
            _resumeButton.Clicked += ResumeButtonClicked;
            _pauseButton.Clicked += PauseButtonClicked;
            _restartLevelButton.Clicked += RestartLevelButtonClicked;
            _returnToMainMenuButton.Clicked += ReturnToMainMenuButtonClicked;
        }

        private void OnDisable()
        {
            _resumeButton.Clicked -= ResumeButtonClicked;
            _pauseButton.Clicked -= PauseButtonClicked;
            _restartLevelButton.Clicked -= RestartLevelButtonClicked;
            _returnToMainMenuButton.Clicked -= ReturnToMainMenuButtonClicked;
        }
    }
}
