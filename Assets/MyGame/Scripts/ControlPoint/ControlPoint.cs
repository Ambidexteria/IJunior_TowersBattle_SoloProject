using Base.GameLogic;
using Base.Soldier;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ControlPoint : MonoBehaviour, ISelectable
{
    [SerializeField] private Renderer _flag;
    [SerializeField] private TeamType _team = TeamType.None;
    [SerializeField] private Material _playerColor;
    [SerializeField] private Material _npcColor;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private int _energyRate = 1;

    public event Action<ControlPoint> Captured;

    public TeamType Team => _team;
    public int EnergyRate => _energyRate;

    private void Awake()
    {
        _flag.material = _defaultMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SoldierSetup soldier))
        {
            TeamType team = soldier.GetSoldier().GetTeam();

            if (_team != team)
                ChangeTeam(team);
        }
    }

    private void ChangeTeam(TeamType team)
    {
        _team = team;
        
        if(_team == TeamType.Player)
            _flag.material = _playerColor;
        else if(_team == TeamType.NPC)
            _flag.material = _npcColor;
        else
            _flag.material = _defaultMaterial;

        Captured?.Invoke(this);
    }
}
