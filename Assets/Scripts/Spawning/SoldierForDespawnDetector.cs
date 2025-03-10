using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SoldierForDespawnDetector : MonoBehaviour
{
    public event Action<Soldier> Detected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Soldier soldier))
        {
            Debug.Log($"{soldier.name} is detected");
            Detected?.Invoke(soldier);
        }
    }
}
