using System;

public class NPC
{
    private readonly NPCCannonController _cannonController;
    private readonly NPCSoldierController _soldierController;
    private readonly SoldierSpawnControllerModel _soldierSpawnController;

    private bool _enabled = false;

    public int CannonDamageTaken => _cannonController.CannonDamageTaken;

    public event Action Defeated;

    public NPC(NPCCannonController cannonController, NPCSoldierController soldierController, SoldierSpawnControllerModel soldierSpawnController)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(NPC), cannonController, soldierController, soldierSpawnController);

        _cannonController = cannonController;
        _soldierController = soldierController;
        _soldierSpawnController = soldierSpawnController;
    }

    public void Enable()
    {
        if(_enabled) 
            return;

        _cannonController.Enable();
        _soldierSpawnController.Enable();
        _soldierController.Enable();

        _cannonController.CannonDestroyed += OnDefeated;

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

    private void OnDefeated()
    {
        Defeated?.Invoke();
    }
}
