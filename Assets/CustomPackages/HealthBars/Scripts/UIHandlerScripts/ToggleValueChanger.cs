using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleValueChanger : MonoBehaviour
{
    private Toggle _toggle;

    public Action<bool> ValueChanged;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(ChangeValue);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ChangeValue);
    }

    private void ChangeValue(bool value)
    {
        ValueChanged?.Invoke(value);
    }
}