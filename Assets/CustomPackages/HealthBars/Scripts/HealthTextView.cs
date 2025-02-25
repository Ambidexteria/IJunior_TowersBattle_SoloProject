using System;
using UnityEngine;
using TMPro;

public class HealthTextView : HealthView
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        if (_text == null)
            throw new ArgumentNullException();
    }

    public override void Display(float value)
    {
        string text = $"{(int)value} / {(int)GetMaxHealth()}";
        _text.text = text;
    }
}