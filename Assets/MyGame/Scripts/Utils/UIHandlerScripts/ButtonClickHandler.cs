using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();

        ExceptionsTest.NullRefMethodTest(nameof(ButtonClickHandler), nameof(Awake), _button);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LaunchAction);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LaunchAction);
    }

    public void Enable()
    {
        _button.interactable = true;
    }

    public void Disable()
    {
        _button.interactable = false;
    }

    private void LaunchAction()
    {
        Clicked?.Invoke();
    }
}