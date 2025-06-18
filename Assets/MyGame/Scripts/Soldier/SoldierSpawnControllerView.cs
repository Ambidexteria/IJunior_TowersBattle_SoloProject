using TMPro;
using UnityEngine;

namespace Base
{
    public class SoldierSpawnControllerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(SoldierSpawnControllerView), nameof(Awake), _text);
        }

        public void Display(float timeBeforeNextSpawn)
        {
            _text.text = string.Format("{0:f2}", timeBeforeNextSpawn);
        }
    }
}