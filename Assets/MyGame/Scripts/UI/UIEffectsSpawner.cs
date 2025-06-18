using UnityEngine;

public class UIEffectsSpawner : MonoBehaviour
{
    [SerializeField] private float _distanceFromCamera = 10f;
    [SerializeField] private ParticleSystemController _effectController;

    private Vector3 _position;

    private void Awake()
    {
        ExceptionsTest.NullRefMethodTest(nameof(UIEffectsSpawner), nameof(Awake), _effectController);

        Vector3 direction = Camera.main.transform.forward;

        _position = Camera.main.transform.position + direction * _distanceFromCamera;

        _effectController.SetPosition(_position);
    }
}
