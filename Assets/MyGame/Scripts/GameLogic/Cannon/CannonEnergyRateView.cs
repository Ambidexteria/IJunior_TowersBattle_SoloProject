using TMPro;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyRateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _energyRate;

        public void SetValue(int energyRate)
        {
            _energyRate.text = energyRate.ToString();
        }
    }
}
