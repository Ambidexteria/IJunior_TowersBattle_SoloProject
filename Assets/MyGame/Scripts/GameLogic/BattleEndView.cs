using System;
using TMPro;
using UnityEngine;

namespace Base.GameLogic
{
    public class BattleEndView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _homeButton;
        [SerializeField] private ButtonClickHandler _nextStageButton;
        [SerializeField] private UIWindowController _winMessage;
        [SerializeField] private UIWindowController _defeatMessage;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _earnedGoldText;

        public event Action HomeButtonClicked;
        public event Action NextStageButtonClicked;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(BattleEndView), nameof(Awake), _homeButton, _winMessage, _defeatMessage, 
                _score, _earnedGoldText);
        }

        public void Enable()
        {
            _homeButton.Clicked += OnHomeButtonClicked;
            _nextStageButton.Clicked += OnNextStageButtonClicked;
        }

        public void Disable()
        {
            _homeButton.Clicked -= OnHomeButtonClicked;
            _nextStageButton.Clicked -= OnNextStageButtonClicked;
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

        public void ShowNextStageButton()
        {
            _nextStageButton.gameObject.SetActive(true);
        }

        private void OnHomeButtonClicked()
        {
            HomeButtonClicked?.Invoke();
        }

        private void OnNextStageButtonClicked()
        {
            NextStageButtonClicked?.Invoke();
        }
    }
}
