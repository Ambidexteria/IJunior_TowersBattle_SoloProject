using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Renderer))]
public class TeamColorChanger : MonoBehaviour
{
    private Team _team;
    private Renderer _renderer;
    private TeamColorDatabase _database;

    [Inject]
    private void Init(TeamColorDatabase teamColorDatabase)
    {
        _database = teamColorDatabase;

        _renderer = GetComponent<Renderer>();
    }

    public void Recolor(Team team)
    {
        _renderer.material = _database.GetMaterialByTeamType(team.Type);
    }
}
