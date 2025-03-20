using UnityEngine;

public class NPCCannonController : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private CannonEnergyBar _energyBar;

    private void OnEnable()
    {
        _energyBar.Filled += OnEnergyBarFilled;
    }

    private void OnDisable()
    {
        _energyBar.Filled -= OnEnergyBarFilled;
    }

    private void OnEnergyBarFilled()
    {
        _cannon.Shoot();
    }
}
