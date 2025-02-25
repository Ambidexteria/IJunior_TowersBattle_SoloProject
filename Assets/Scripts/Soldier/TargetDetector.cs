using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TargetDetector : MonoBehaviour
{
    public event Action<ITargetSoldier> Detected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITargetSoldier target))
            Detected?.Invoke(target);
    }
}
