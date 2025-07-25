using Base.GameLogic.Cannon;
using Base.GameLogic.ShootMinigame;
using System;
using System.Collections.Generic;

public class Player
{
    private readonly Team _team;
    private readonly CannonModel _cannon;
    private readonly CannonEnergyBarModel _energyBar;
    private readonly ShootMinigameModel _shootMinigame;
    private readonly SoldierSpawnControllerModel _soldierSpawnerController;
    private readonly SoldierCommandController _commandController;
    private readonly SoldierSelector _soldierSelector;
    private readonly int _selfDamage;

    private bool _enabled = false;

    public Player(Team team, CannonModel cannon, CannonEnergyBarModel energyBar, ShootMinigameModel shootMinigame, 
        SoldierSpawnControllerModel soldierSpawnerController, SoldierCommandController commandController, SoldierSelector soldierSelector, int selfDamage = 5)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(Player), cannon, energyBar, shootMinigame, 
            soldierSpawnerController, commandController);

        _team = team;
        _cannon = cannon;
        _energyBar = energyBar;
        _shootMinigame = shootMinigame;
        _soldierSpawnerController = soldierSpawnerController;
        _commandController = commandController;
        _soldierSelector = soldierSelector;
        _selfDamage = selfDamage;
    }

    public SoldierCommandController SoldierCommandController => _commandController;
    public SoldierSelector SoldierSelector => _soldierSelector;
    public CannonEnergyBarModel CannonEnergyBar => _energyBar;
    public Team Team => _team;

    public event Action Defeated;
    public event Action<SoldierModel> SoldiersSpawned;
    public event Action<bool> ShooMinigameWinned;

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
        _soldierSelector.Enable();
        //_commandController.Enable();
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
        _soldierSelector.Disable();
        //_commandController.Disable();
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
        ShooMinigameWinned?.Invoke(true);
    }

    private void OnLooseMinigame()
    {
        _cannon.TakeDamage(_selfDamage);
        EndMinigame();
        ShooMinigameWinned?.Invoke(false);
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
