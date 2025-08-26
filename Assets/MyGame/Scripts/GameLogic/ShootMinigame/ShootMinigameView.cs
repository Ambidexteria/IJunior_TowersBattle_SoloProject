using System;
using Lean.Localization;
using UnityEngine;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameView : MonoBehaviour
    {
        private const string MinigameReadyPhrase = "play minigame button";
        private const string ReloadingPhrase = "reloading";

        [SerializeField] private LeanLocalizedTextMeshProUGUI _text;
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
            _text.TranslationName = MinigameReadyPhrase;
        }

        private void OnShootButtonClicked()
        {
            ShootButtonClicked?.Invoke();
        }

        private void OnLaunchButtonClicked()
        {
            LaunchButtonClicked?.Invoke();
            _launchMinigameButton.Disable();
            _text.TranslationName = ReloadingPhrase;
        }
    }
}
