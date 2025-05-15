using Base.GameLogic.Cannon;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CannonModel _cannon;
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
        _soldierSpawnerController.StopSpawn();
        _soldierSelector.enabled = false;
        _energyBar.enabled = false;
    }

    private void OnEnergyBarFilled()
    {
        if (_shootMinigame.Activated)
            return;

        _shootMinigame.Activate();
    }

    private void OnWinMinigame()
    {
        _cannon.Shoot();
        EndMinigame();
    }

    private void OnLooseMinigame()
    {
        _cannon.TakeDamage(_cannon.Damage);
        EndMinigame();
    }

    private void EndMinigame()
    {
        _energyBar.RemoveCurrentEnergy();
    }

    private void OnCannonDestroyed()
    {
        Defeated?.Invoke();
        Stop();
    }
}
