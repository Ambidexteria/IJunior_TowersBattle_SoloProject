using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TeamColorChanger : MonoBehaviour
{
    [SerializeField] private List<ColorChangerMark> _marks = new();

    private TeamColorDatabase _database;

    [Inject]
    private void Init(TeamColorDatabase teamColorDatabase)
    {
        _database = teamColorDatabase;
    }

    public void Recolor(Team team)
    {
        Material material = _database.GetMaterialByTeamType(team.Type);

        foreach (var mark in _marks)
            mark.SetMaterial(material);
    }
}
