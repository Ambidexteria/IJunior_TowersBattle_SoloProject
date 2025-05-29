using System;
using TMPro;
using UnityEngine;

namespace Base.GameLogic
{
    public class BattleEndView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _homeButton;
        [SerializeField] private UIWindowController _winMessage;
        [SerializeField] private UIWindowController _defeatMessage;
        [SerializeField] private TextMeshProUGUI _currentGoldText;
        [SerializeField] private TextMeshProUGUI _earnedGoldText;

        public event Action HomeButtonClicked;

        public void Enable()
        {
            _homeButton.Clicked += OnHomeButtonClicked;
        }

        public void Disable()
        {
            _homeButton.Clicked -= OnHomeButtonClicked;
        }

        public void ShowCurrentGold(int amount)
        {
            _currentGoldText.text = amount.ToString();
        }

        public void ShowEarnedGold(int amount)
        {
            _earnedGoldText.text = amount.ToString();
        }

        public void ShowWinMessage()
        {
            _winMessage.Show();
        }

        public void ShowDefeatMessage()
        {
            _defeatMessage.Show();
        }

        private void OnHomeButtonClicked()
        {
            HomeButtonClicked?.Invoke();
        }
    }
}
