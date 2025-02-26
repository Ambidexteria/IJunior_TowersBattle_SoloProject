using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCSoldierController : MonoBehaviour
{
    [SerializeField] private List<ControlPoint> _controlPoints = new List<ControlPoint>();
    [SerializeField] private List<Soldier> _soldiers = new List<Soldier>();

    [SerializeField] private float _nextCommandDelay = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SendSoldierToControlPoint), 0f, _nextCommandDelay);
    }

    private void SendSoldierToControlPoint()
    {
        Soldier soldier = _soldiers[Random.Range(0, _soldiers.Count)];

        if (TryGetNearestVacantControlPoint(soldier, out var controlPoint))
        {
            soldier.MoveTo(controlPoint.transform);
            Debug.Log("Soldier has been sended");
        }
        else
        {
            Debug.LogError("Cannot Send Soldier");
        }
    }

    private bool TryGetNearestVacantControlPoint(Soldier soldier, out ControlPoint controlPoint)
    {
        controlPoint = null;

        var vacantControlPoints = _controlPoints.Where(x => x.Team != Team.NPC).OrderBy(x => GetDistanceBetween(soldier, x)).ToList();

        if (vacantControlPoints.Count > 0)
        {
            controlPoint = vacantControlPoints[0];
            return true;
        }

        return false;
    }

    private float GetDistanceBetween(Soldier soldier, ControlPoint controlPoint)
    {
        return (soldier.transform.position - controlPoint.transform.position).sqrMagnitude;
    }
}
