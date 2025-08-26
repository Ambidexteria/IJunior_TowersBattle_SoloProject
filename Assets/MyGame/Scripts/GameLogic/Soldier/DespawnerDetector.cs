using System;
using UnityEngine;
using Base.Logic;

public class DespawnerDetector
{
    private TriggerObserver _trigger;

    public DespawnerDetector(TriggerObserver trigger)
    {
        _trigger = trigger;
        _trigger.Entered += OnTriggerEntered;
    }

    public event Action Detected;

    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out SoldierForDespawnDetector _))
            Detected?.Invoke();
    }
}
