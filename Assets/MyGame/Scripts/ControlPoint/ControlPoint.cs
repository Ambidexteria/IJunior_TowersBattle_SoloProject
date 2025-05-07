using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class ControlPoint : MonoBehaviour
{
    [SerializeField] private TeamType _team = TeamType.None;
    [SerializeField] private Material _playerColor;
    [SerializeField] private Material _npcColor;
    [SerializeField] private int _energyRate = 1;

    private Renderer _renderer;
    private Material _defaultMaterial;

    public event Action<ControlPoint> Captured;

    public TeamType Team => _team;
    public int EnergyRate => _energyRate;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultMaterial = _renderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ISoldier soldier))
        {
            TeamType team = soldier.GetTeam();

            if (_team != team)
                ChangeTeam(team);
        }
    }

    private void ChangeTeam(TeamType team)
    {
        _team = team;
        
        if(_team == TeamType.Player)
            _renderer.material = _playerColor;
        else if(_team == TeamType.NPC)
            _renderer.material = _npcColor;
        else
            _renderer.material = _defaultMaterial;

        Captured?.Invoke(this);
    }
}
