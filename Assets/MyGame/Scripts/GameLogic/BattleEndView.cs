using TMPro;
using UnityEngine;

namespace Base.GameLogic
{
    public class BattleEndView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentGoldText;
        [SerializeField] private TextMeshProUGUI _earnedGoldText;

        public void ShowCurrentGold(int amount)
        {
            _currentGoldText.text = amount.ToString();
        }

        public void ShowEarnedGold(int amount)
        {
            _earnedGoldText.text = amount.ToString();
        }
    }
}
