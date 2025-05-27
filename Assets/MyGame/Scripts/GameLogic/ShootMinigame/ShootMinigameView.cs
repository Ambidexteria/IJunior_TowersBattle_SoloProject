using System;
using UnityEngine;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _launchMinigameButton;
        [SerializeField] private ButtonClickHandler _shootButton;

        public event Action LaunchButtonClicked;
        public event Action ShootButtonClicked;

        private void OnEnable()
        {
            _launchMinigameButton.Clicked += OnLaunchButtonClicked;
            _shootButton.Clicked += OnShootButtonClicked;
        }

        private void OnDisable()
        {
            _launchMinigameButton.Clicked -= OnLaunchButtonClicked;
            _shootButton.Clicked -= OnShootButtonClicked;
        }

        public void EnableLaunchButton()
        {
            _launchMinigameButton.Enable();
        }

        private void OnShootButtonClicked()
        {
            ShootButtonClicked?.Invoke();
        }

        private void OnLaunchButtonClicked()
        {
            LaunchButtonClicked?.Invoke();
            _launchMinigameButton.Disable();
        }
    }
}
