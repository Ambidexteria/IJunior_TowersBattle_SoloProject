using Base.GameLogic.Cannon;
using Base.GameLogic.ShootMinigame;
using System;
using System.Collections.Generic;

public class Player
{
    private readonly CannonModel _cannon;
    private readonly CannonEnergyBarModel _energyBar;
    private readonly ShootMinigameModel _shootMinigame;
    private readonly SoldierSpawnControllerModel _soldierSpawnerController;
    private readonly SoldierCommandController _commandController;
    private bool _enabled = false;

    public Player(CannonModel cannon, CannonEnergyBarModel energyBar, ShootMinigameModel shootMinigame, 
        SoldierSpawnControllerModel soldierSpawnerController, SoldierCommandController commandController)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(Player), cannon, energyBar, shootMinigame, 
            soldierSpawnerController, commandController);

        _cannon = cannon;
        _energyBar = energyBar;
        _shootMinigame = shootMinigame;
        _soldierSpawnerController = soldierSpawnerController;
        _commandController = commandController;
    }

    public SoldierCommandController SoldierCommandController => _commandController;
    public CannonEnergyBarModel CannonEnergyBar => _energyBar;

    public event Action Defeated;
    public event Action<SoldierModel> SoldiersSpawned;

    public void Enable()
    {
        if(_enabled) 
            return;

        _cannon.Destroyed += OnCannonDestroyed;
        _soldierSpawnerController.Spawned += OnSoldierSpawned;
        _shootMinigame.Winned += OnWinMinigame;
        _shootMinigame.Loosed += OnLooseMinigame;
        
        _cannon.Enable();
        _energyBar.Enable();
        _soldierSpawnerController.Enable();
        _commandController.Enable();
        _shootMinigame.Enable();

        _enabled = true;
    }

    public void Disable()
    {
        if (_enabled == false)
            return;

        _cannon.Destroyed -= OnCannonDestroyed;
        _soldierSpawnerController.Spawned -= OnSoldierSpawned;
        _shootMinigame.Winned -= OnWinMinigame;
        _shootMinigame.Loosed -= OnLooseMinigame;

        _cannon.Disable();
        _energyBar.Disable();
        _soldierSpawnerController.Disable();
        _commandController.Disable();
        _shootMinigame.Disable();

        _enabled = false;
    }

    public void StartSpawningSoldiers()
    {
        _soldierSpawnerController.Enable();
    }

    public void StopSpawningSoldiers()
    {
        _soldierSpawnerController.Disable();
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
    }

    private void OnSoldierSpawned(SoldierModel soldierModel)
    {
        SoldiersSpawned?.Invoke(soldierModel);
    }
}
