using UnityEngine;
using TMPro;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarView : MonoBehaviour
    {

        [SerializeField] private SliderValueChanger _sliderValueChanger;
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

        private float _maxEnergy;

        public void Init(float  maxEnergy)
        {
            _maxEnergy = maxEnergy;
            _sliderValueChanger.SetMinMaxValues(0, _maxEnergy);
            _textMeshProUGUI.text = $"{0} / {_maxEnergy}";
        }

        public void Display(float amount)
        {
            _sliderValueChanger.SetValue(amount);
            _textMeshProUGUI.text = $"{(int)amount} / {_maxEnergy}";
        }
    }
}
