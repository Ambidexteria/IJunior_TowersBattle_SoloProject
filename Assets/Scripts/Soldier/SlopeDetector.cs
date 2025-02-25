using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SlopeDetector : MonoBehaviour
{
    [SerializeField, Range(10, 60)] private int _slopeAngle = 45;
    [SerializeField] private Transform _pointOfRotation;

    private bool _detected = false;

    public bool Detected => _detected;

    private void Awake()
    {
        _pointOfRotation.localRotation = Quaternion.Euler(-_slopeAngle, 0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground _))
        {
            _detected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Ground _))
        {
            _detected = false;
        }
    }
}
