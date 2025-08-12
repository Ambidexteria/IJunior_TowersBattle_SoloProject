using UnityEngine;
using UnityEngine.UI;

namespace Base.Soldier
{
    public class SoldierView : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        private float _maxHealth;

        public void Init(float maxHealth)
        {
            _maxHealth = maxHealth;
        }

        public void DisplayHealth(float health)
        {
            _fill.fillAmount = health / _maxHealth;
        }
    }
}
