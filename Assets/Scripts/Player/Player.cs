using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private CannonEnergyBar _energyBar;
    [SerializeField] private ShootMinigame _shootMinigame;
    [SerializeField] private SoldierSpawnController _soldierSpawnerController;
    [SerializeField] private SoldierSelector _soldierSelector;

    public event Action Defeated;

    private void OnEnable()
    {
        _energyBar.Filled += OnEnergyBarFilled;
        _cannon.Destroyed += OnCannonDestroyed;

        _shootMinigame.Winned += OnWinMinigame;
        _shootMinigame.Loosed += OnLooseMinigame;
    }

    private void OnDisable()
    {
        _energyBar.Filled -= OnEnergyBarFilled;
        _cannon.Destroyed -= OnCannonDestroyed;

        _shootMinigame.Winned -= OnWinMinigame;
        _shootMinigame.Loosed -= OnLooseMinigame;
    }

    public void Stop()
    {
        Debug.Log("Stop");
        _soldierSpawnerController.StopSpawn();
        _soldierSelector.enabled = false;
        _energyBar.enabled = false;
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
        _shootMinigame.gameObject.SetActive(false);
    }

    private void OnLooseMinigame()
    {
        _cannon.TakeDamage(_cannon.Damage);
        _shootMinigame.gameObject.SetActive(false);
    }

    private void OnCannonDestroyed()
    {
        Defeated?.Invoke();
        Stop();
    }
}
