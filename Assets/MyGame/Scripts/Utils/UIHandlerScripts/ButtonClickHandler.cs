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
        var delegates = Clicked.GetInvocationList();
        string text = $"Active delegates on {nameof(Clicked)} in {nameof(ButtonClickHandler)}\n";

        foreach (var delegator in delegates)
            text += delegator.Method.Name + "\n";

        Debug.Log(text);

        Clicked?.Invoke();
    }
}