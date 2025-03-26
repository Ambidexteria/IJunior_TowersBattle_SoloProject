using UnityEngine;
using Zenject;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInput _input;

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    [Inject]
    private void Construct(PlayerInput input)
    {
        _input = input;
    }
}
