using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    public class SoldierSpawnControllerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _fillCircle;

        private float _spawnDelay;

        public void Init(float spawnDelay)
        {
            _spawnDelay = spawnDelay;
        }

        public void Display(float timeBeforeNextSpawn)
        {
            _text.text = string.Format("{0:f1}", timeBeforeNextSpawn);
            _fillCircle.fillAmount = (_spawnDelay - timeBeforeNextSpawn) / _spawnDelay;
        }
    }
}