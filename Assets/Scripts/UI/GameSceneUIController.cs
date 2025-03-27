using UnityEngine;

public class GameSceneUIController : MonoBehaviour
{
    [SerializeField] private ShootMinigame _shootMinigame;
    [SerializeField] private ToggleValueChanger _settingsButton;
    [SerializeField] private UIWindowController _settingsWindow;
    [SerializeField] private UIWindowController _winMessage;
    [SerializeField] private UIWindowController _defeatMessage;

    private void OnEnable()
    {
        _settingsButton.ValueChanged += OnSettingButtonPressed;
    }

    private void OnDisable()
    {
        _settingsButton.ValueChanged -= OnSettingButtonPressed;
    }

    private void OnSettingButtonPressed(bool enable)
    {
        _settingsWindow.gameObject.SetActive(enable);
    }
}
