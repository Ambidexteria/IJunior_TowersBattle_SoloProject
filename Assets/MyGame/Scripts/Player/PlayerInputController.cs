using Base.Services.Input;
using UnityEngine;
using Zenject;

public class PlayerInputController
{
    private InputService _input;

    public PlayerInputController(InputService input)
    {
        _input = input;
    }

    public void Enable()
    {
        _input.Enable();
    }

    public void Disable()
    {
        _input.Disable();
    }
}
