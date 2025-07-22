using System;
using UnityEngine;

public class NPC
{
    private readonly NPCCannonController _cannonController;
    private readonly NPCSoldierController _soldierController;
    private readonly SoldierSpawnControllerModel _soldierSpawnController;

    private bool _enabled = false;

    public NPC(NPCCannonController cannonController, NPCSoldierController soldierController, SoldierSpawnControllerModel soldierSpawnController)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(NPC), cannonController, soldierController, soldierSpawnController);

        _cannonController = cannonController;
        _soldierController = soldierController;
        _soldierSpawnController = soldierSpawnController;
    }

    public int CannonDamageTaken => _cannonController.CannonDamageTaken;

    public event Action Defeated;
    public event Action SoldierSpawned;
    public event Action CannonShooted;

    public void Enable()
    {
        if(_enabled) 
            return;

        _cannonController.Enable();
        _soldierSpawnController.Enable();
        _soldierController.Enable();

        _cannonController.CannonDestroyed += OnDefeated;
        _cannonController.CannonShooted += OnCannonShooted;
        _soldierSpawnController.Spawned += OnSoldierSpawned;

        _enabled = true;
    }

    public void Disable()
    {
        if (_enabled == false)
            return;

        _soldierController.Disable();
        _soldierSpawnController.Disable();
        _cannonController.Disable();

        _cannonController.CannonDestroyed -= OnDefeated;

        _enabled = false;
    }

    public void StartSpawningSoldiers()
    {
        _soldierSpawnController.Enable();
    }

    public void StopSpawningSoldiers()
    {
        _soldierSpawnController.Disable();
    }

    public void EnableCannon()
    {
        _cannonController.Enable();
    }

    public void DisableCannon()
    {
        _cannonController.Disable();
    }

    private void OnDefeated()
    {
        Defeated?.Invoke();
    }

    private void OnCannonShooted()
    {
        CannonShooted?.Invoke();
    }

    private void OnSoldierSpawned(SoldierModel model)
    {
        SoldierSpawned?.Invoke();
    }
}
