using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class ControlPoint : MonoBehaviour
{
    [SerializeField] private Team _team = Team.None;
    [SerializeField] private Material _playerColor;
    [SerializeField] private Material _npcColor;

    private Renderer _renderer;
    private Material _defaultMaterial;

    public Team Team => _team;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultMaterial = _renderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITargetSoldier soldier))
        {
            Team team = soldier.GetTeam();

            if (_team != team)
                ChangeTeam(team);
        }
    }

    private void ChangeTeam(Team team)
    {
        _team = team;
        
        if(_team == Team.Player)
            _renderer.material = _playerColor;
        else if(_team == Team.NPC)
            _renderer.material = _npcColor;
        else
            _renderer.material = _defaultMaterial;
    }
}
