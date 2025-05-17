using System;

public class NPC
{
    private NPCCannonController _cannonController;
    private NPCSoldierController _soldierController;
    private SoldierSpawnControllerModel _soldierSpawnController;

    public event Action Defeated;

    public NPC(NPCCannonController cannonController, NPCSoldierController soldierController, SoldierSpawnControllerModel soldierSpawnController)
    {
        _cannonController = cannonController;
        _soldierController = soldierController;
        _soldierSpawnController = soldierSpawnController;
    }

    public void Enable()
    {
        _cannonController.Enable();
        _soldierSpawnController.Enable();
        _soldierController.Enable();

        _cannonController.CannonDestroyed += OnDefeated;
    }

    public void Stop()
    {
        _soldierController.Disable();
        _soldierSpawnController.Disable();
        _cannonController.Disable();

        _cannonController.CannonDestroyed -= OnDefeated;
    }

    private void OnDefeated()
    {
        Defeated?.Invoke();
    }
}
