using Base.GameLogic.Cannon;
using Base.GameLogic.ShootMinigame;
using System;
using UnityEngine;

public class Player
{
    private CannonModel _cannon;
    private CannonEnergyBar _energyBar;
    private ShootMinigameModel _shootMinigame;
    private SoldierSpawnControllerModel _soldierSpawnerController;

    private bool _enabled = false;

    public Player(CannonModel cannon, CannonEnergyBar energyBar, ShootMinigameModel shootMinigame, 
        SoldierSpawnControllerModel soldierSpawnerController)
    {
        _cannon = cannon;
        _energyBar = energyBar;
        _shootMinigame = shootMinigame;
        _soldierSpawnerController = soldierSpawnerController;
    }

    public event Action Defeated;

    public void Enable()
    {
        if(_enabled) 
            return;

        _energyBar.Filled += OnEnergyBarFilled;
        _cannon.Destroyed += OnCannonDestroyed;

        _shootMinigame.Winned += OnWinMinigame;
        _shootMinigame.Loosed += OnLooseMinigame;

        _soldierSpawnerController.Enable();

        _enabled = true;
    }

    public void Disable()
    {
        _energyBar.Filled -= OnEnergyBarFilled;
        _cannon.Destroyed -= OnCannonDestroyed;

        _shootMinigame.Winned -= OnWinMinigame;
        _shootMinigame.Loosed -= OnLooseMinigame;

        _soldierSpawnerController.Disable();

        _enabled = false;
    }

    public void Stop()
    {
        _soldierSpawnerController.Disable();
        _energyBar.Disable();
    }

    private void OnEnergyBarFilled()
    {
        if (_shootMinigame.Activated)
            return;

        _shootMinigame.LaunchMinigame();
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
