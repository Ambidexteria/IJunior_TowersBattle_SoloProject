using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarView : MonoBehaviour
    {
        [SerializeField] private SliderValueChanger _sliderValueChanger;

        private float _maxEnergy;

        public void SetMaxEnergy(float maxEnergy)
        {
            _maxEnergy = maxEnergy;
            _sliderValueChanger.SetMinMaxValues(0, _maxEnergy);
        }

        public void Display(float amount)
        {
            _sliderValueChanger.SetValue(amount);
        }
    }
}
