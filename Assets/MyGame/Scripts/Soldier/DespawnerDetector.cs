using Base.Logic;
using System;
using UnityEngine;

public class DespawnerDetector
{
    private TriggerObserver _trigger;

    public DespawnerDetector(TriggerObserver trigger)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(DespawnerDetector), trigger);

        _trigger = trigger;
        _trigger.Entered += OnTriggerEntered;
    }

    public event Action Detected;

    private void OnTriggerEntered(Collider other)
    {
        ExceptionsTest.NullRefMethodTest(nameof(DespawnerDetector), nameof(OnTriggerEntered), other);

        if (other.TryGetComponent(out SoldierForDespawnDetector _))
            Detected?.Invoke();
    }
}
