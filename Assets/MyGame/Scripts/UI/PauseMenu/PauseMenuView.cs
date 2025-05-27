using System;
using UnityEngine;

namespace Base
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _restartLevelButton;
        [SerializeField] private ButtonClickHandler _returnToMainMenuButton;

        public event Action ResumeButtonClicked;
        public event Action PauseButtonClicked;
        public event Action RestartLevelButtonClicked;
        public event Action ReturnToMainMenuButtonClicked;

        public void Enable()
        {
            _restartLevelButton.Clicked += RestartLevelButtonClicked;
            _returnToMainMenuButton.Clicked += ReturnToMainMenuButtonClicked;
        }

        public void Disable()
        {
            _restartLevelButton.Clicked += RestartLevelButtonClicked;
            _returnToMainMenuButton.Clicked += ReturnToMainMenuButtonClicked;
        }
    }
}
