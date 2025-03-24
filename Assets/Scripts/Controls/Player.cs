using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private ShootMinigame _shootMinigame;

    private void OnEnable()
    {
        _cannon.EnergyBarFilled += OnEnergyBarFilled;

        _shootMinigame.Winned += OnWinMinigame;
        _shootMinigame.Loosed += OnLooseMinigame;
    }

    private void OnDisable()
    {
        _cannon.EnergyBarFilled -= OnEnergyBarFilled;

        _shootMinigame.Winned -= OnWinMinigame;
        _shootMinigame.Loosed -= OnLooseMinigame;
    }

    private void OnEnergyBarFilled()
    {
        if (_shootMinigame.gameObject.activeInHierarchy)
            return;

        _shootMinigame.gameObject.SetActive(true);
        _shootMinigame.Launch();
    }

    private void OnWinMinigame()
    {
        _cannon.Shoot();

        Debug.Log("Win!!!");
        _shootMinigame.gameObject.SetActive(false);
    }

    private void OnLooseMinigame()
    {
        _cannon.TakeDamage(_cannon.Damage);
        Debug.LogError($"Looose!!! {_cannon.Damage} damage taken");
        _shootMinigame.gameObject.SetActive(false);
    }
}
