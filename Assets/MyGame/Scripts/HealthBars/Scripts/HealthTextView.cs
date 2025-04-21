using System;
using UnityEngine;
using TMPro;

public class HealthTextView : CannonHealthView
{
    [SerializeField] private TextMeshProUGUI _text;

    public override void PrepareOnAwake()
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