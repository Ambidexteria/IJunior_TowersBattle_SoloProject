using UnityEngine;

public class UIDisabler : MonoBehaviour
{
    [SerializeField] private GameObject[] _uiGameobjects;
    [SerializeField] private KeyCode _toggleKey;

    private bool _enabled = true;

    private void Update()
    {
        if (Input.GetKeyDown(_toggleKey))
        {
            if (_enabled)
            {
                foreach (var item in _uiGameobjects)
                    item.SetActive(false);

                _enabled = false;
            }
            else
            {
                foreach (var item in _uiGameobjects)
                    item.SetActive(true);

                _enabled = true;
            }
        }
    }
}
