using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCCannonController _cannonController;
    [SerializeField] private NPCSoldierController _soldierController;
    [SerializeField] private SoldierSpawnController _soldierSpawnController;

    public event Action Defeated;

    private void OnEnable()
    {
        _cannonController.CannonDestroyed += OnDefeated;
    }

    private void OnDisable()
    {
        _cannonController.CannonDestroyed -= OnDefeated;
    }

    public void Stop()
    {
        _soldierController.StopSendingSoldiers();
        _soldierSpawnController.StopSpawn();
    }

    private void OnDefeated()
    {
        Defeated?.Invoke();

        Debug.Log("NPC defeated");
    }
}
