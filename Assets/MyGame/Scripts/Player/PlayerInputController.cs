using Base.Services.Input;
using UnityEngine;
using Zenject;

public class PlayerInputController : MonoBehaviour
{
    private InputService _input;

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    [Inject]
    private void Construct(InputService input)
    {
        _input = input;
    }
}
