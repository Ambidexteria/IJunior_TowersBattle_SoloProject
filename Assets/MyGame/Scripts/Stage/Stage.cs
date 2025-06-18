using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<ControlPoint> _controlPoints;
    [SerializeField] private Transform _playerSoldierSpawnPoint;
    [SerializeField] private Transform _npcSoldierSpawnPoint;

    public Transform PlayerSoldierSpawnPoint => _playerSoldierSpawnPoint;
    public Transform NPCSoldierSpawnPoint => _npcSoldierSpawnPoint;

    private void Awake()
    {
        ExceptionsTest.NullRefMethodTest(nameof(Stage), nameof(Awake), _controlPoints, _playerSoldierSpawnPoint, _npcSoldierSpawnPoint);
        ExceptionsTest.EmptyListTest(nameof(Stage), nameof(Awake), _controlPoints);
    }

    public List<ControlPoint> GetControlPoints()
    {
        return new List<ControlPoint>(_controlPoints);
    }
}
