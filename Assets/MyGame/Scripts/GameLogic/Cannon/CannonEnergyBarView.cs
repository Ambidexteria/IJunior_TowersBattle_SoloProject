using UnityEngine;
using TMPro;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarView : MonoBehaviour
    {

        [SerializeField] private SliderValueChanger _sliderValueChanger;
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] private CannonEnergyBar _energyBar;

        private float _maxEnergy;

        private void Awake()
        {
            _maxEnergy = _energyBar.MaxEnergy;
            _sliderValueChanger.SetMinMaxValues(0, _maxEnergy);
            _textMeshProUGUI.text = $"{0} / {_maxEnergy}";
        }

        private void OnEnable()
        {
            _energyBar.CurrentEnergyChanged += OnEnergyChanged;
        }

        private void OnDisable()
        {
            _energyBar.CurrentEnergyChanged -= OnEnergyChanged;
        }

        private void OnEnergyChanged(float energy)
        {
            _sliderValueChanger.SetValue(energy);
            _textMeshProUGUI.text = $"{(int)energy} / {_maxEnergy}";
        }
    }
}
