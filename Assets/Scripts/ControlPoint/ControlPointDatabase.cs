using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointDatabase : MonoBehaviour
{
    [SerializeField] private List<ControlPoint> _controlPoints;

    public event Action<ControlPoint> ControlPointCaptured;

    private void Awake()
    {
        ScanLevelForControlPoints();
    }

    private void OnEnable()
    {
        SubscribeToControlPoints();
    }

    private void OnDisable()
    {
        UnsubscribeToControlPoints();
    }

    private void OnControlPointCaptured(ControlPoint controlpoint)
    {
        ControlPointCaptured?.Invoke(controlpoint);
    }

    private void ScanLevelForControlPoints()
    {
       var controlPoints = FindObjectsOfType<ControlPoint>();
        _controlPoints.AddRange(controlPoints);
    }

    private void SubscribeToControlPoints()
    {
        foreach (var controlPoint in _controlPoints)
        {
            controlPoint.Captured += OnControlPointCaptured;
        }
    }

    private void UnsubscribeToControlPoints()
    {
        foreach (var controlPoint in _controlPoints)
        {
            controlPoint.Captured -= OnControlPointCaptured;
        }
    }
}
