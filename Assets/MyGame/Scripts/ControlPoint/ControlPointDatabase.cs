using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlPointDatabase : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnSoldierPosition;

    private List<ControlPoint> _controlPoints = new();

    public event Action<ControlPoint> ControlPointCaptured;

    private void OnEnable()
    {
        SubscribeToControlPoints();
    }

    private void OnDisable()
    {
        UnsubscribeToControlPoints();
    }

    public void Init(List<ControlPoint> controlPoints, Transform playerSpawnSoldierPosition)
    {
        ExceptionsTest.NullRefMethodTest(nameof(ControlPointDatabase), nameof(Init), controlPoints);
        ExceptionsTest.EmptyListTest(nameof(ControlPointDatabase), nameof(Init), controlPoints);

        _controlPoints = controlPoints;
        _playerSpawnSoldierPosition = playerSpawnSoldierPosition;

        SubscribeToControlPoints();
    }

    public ControlPoint GetClosestControlPointToPlayer()
    {
        if (TryGetNearestVacantControlPoint(TeamType.Player, _playerSpawnSoldierPosition.position, out ControlPoint controlPoint) == false)
            throw new NullReferenceException(nameof(controlPoint));

        return controlPoint;
    }

    public bool TryGetNearestVacantControlPoint(TeamType team, Vector3 position, out ControlPoint controlPoint)
    {
        controlPoint = null;

        var vacantControlPoints = _controlPoints.Where(x => x.Team != team).OrderBy(x => GetDistanceBetween(position, x)).ToList();

        if (vacantControlPoints.Count > 0)
        {
            controlPoint = vacantControlPoints[0];
            return true;
        }

        return false;
    }

    private float GetDistanceBetween(Vector3 position, ControlPoint controlPoint)
    {
        return (position - controlPoint.transform.position).sqrMagnitude;
    }

    private void OnControlPointCaptured(ControlPoint controlpoint)
    {
        ControlPointCaptured?.Invoke(controlpoint);
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
