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
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _earnedGoldText;

        public event Action HomeButtonClicked;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(BattleEndView), nameof(Awake), _homeButton, _winMessage, _defeatMessage, 
                _score, _earnedGoldText);
        }

        public void Enable()
        {
            _homeButton.Clicked += OnHomeButtonClicked;
        }

        public void Disable()
        {
            _homeButton.Clicked -= OnHomeButtonClicked;
        }

        public void ShowScore(int amount)
        {
            _score.text = amount.ToString();
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
