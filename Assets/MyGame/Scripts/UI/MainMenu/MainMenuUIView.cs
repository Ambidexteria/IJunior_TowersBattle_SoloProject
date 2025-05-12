using Base.Data;
using Base.Infrastructure;
using System;
using UnityEngine;
using Zenject;

namespace Base.UI.MainMenu
{
    public class MainMenuUIView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _startBattleButton;

        [SerializeField] private UIWindowController _settingsWindow;
        [SerializeField] private ToggleValueChanger _settingsWindowToggle;

        [SerializeField] private UIWindowController _stagesWindow;
        [SerializeField] private ButtonClickHandler _showStagesButton;
        [SerializeField] private ButtonClickHandler _hideStagesButton;

        [SerializeField] private UIWindowController _shopWindow;
        [SerializeField] private ButtonClickHandler _showShopButton;
        [SerializeField] private ButtonClickHandler _hideShopButton;

        public event Action StartButtonClicked;

        private void OnEnable()
        {
            _startBattleButton.Clicked += OnStartButtonClicked;

            _settingsWindowToggle.ValueChanged += OnSettingsTogglePressed;

            _showStagesButton.Clicked += ShowStagesMenu;
            _hideStagesButton.Clicked += HideStagesMenu;

            _showShopButton.Clicked += ShowShopMenu;
            _hideShopButton.Clicked += HideShopMenu;
        }

        private void OnDisable()
        {
            _startBattleButton.Clicked -= OnStartButtonClicked;

            _settingsWindowToggle.ValueChanged -= OnSettingsTogglePressed;

            _showStagesButton.Clicked -= ShowStagesMenu;
            _hideStagesButton.Clicked -= HideStagesMenu;

            _showShopButton.Clicked -= ShowShopMenu;
            _hideShopButton.Clicked -= HideShopMenu;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnStartButtonClicked()
        {
            Debug.Log("StartButtonClicked");
            StartButtonClicked?.Invoke();
        }

        private void OnSettingsTogglePressed(bool enable)
        {
            _settingsWindow.gameObject.SetActive(enable);
        }

        private void ShowStagesMenu()
        {
            _stagesWindow.Show();
        }

        private void HideStagesMenu()
        {
            _stagesWindow.Hide();
        }

        private void ShowShopMenu()
        {
            _shopWindow.Show();
        }

        private void HideShopMenu()
        {
            _shopWindow.Hide();
        }
    }
}
