using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleValueChanger : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    public Action<bool> ValueChanged;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();

        ExceptionsTest.NullRefMethodTest(nameof(ToggleValueChanger), nameof(Awake), _toggle);
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(ChangeValue);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ChangeValue);
    }

    public void SetValue(bool value)
    {
        _toggle.isOn = value;
    }

    private void ChangeValue(bool value)
    {
        ValueChanged?.Invoke(value);
    }
}