using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GenericDetector<Type> : MonoBehaviour where Type : MonoBehaviour
{
    public event Action<Type> Detected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Type objectOfType))
            Detected?.Invoke(objectOfType);
    }
}
