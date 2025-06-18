using System;
using UnityEngine;

namespace Base
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _restartLevelButton;
        [SerializeField] private ButtonClickHandler _returnToMainMenuButton;

        public event Action RestartLevelButtonClicked;
        public event Action ReturnToMainMenuButtonClicked;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(PauseMenuView), nameof(Awake), _restartLevelButton, _returnToMainMenuButton);
        }

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
