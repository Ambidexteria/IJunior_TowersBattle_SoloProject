using TMPro;
using UnityEngine;

namespace Base
{
    public class SpawnTimeView : MonoBehaviour
    {
        [SerializeField] private SoldierSpawnController _spawnController;
        [SerializeField] private TextMeshProUGUI _text;

        private void Update()
        {
            _text.text = string.Format("{0:f2}", _spawnController.TimeBeforeNextSpawn);
        }
    }

}