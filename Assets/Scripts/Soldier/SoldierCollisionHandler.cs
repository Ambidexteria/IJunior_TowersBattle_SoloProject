using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SoldierCollisionHandler : MonoBehaviour
{
    public event Action DespawnerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SoldierForDespawnDetector _))
        {
            DespawnerDetected?.Invoke();
            Debug.Log("Soldier despawning");
        }
    }
}
