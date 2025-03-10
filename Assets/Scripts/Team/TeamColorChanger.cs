using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TeamColorChanger : MonoBehaviour
{
    private Team _team;
    private TeamColorDatabase _database;
    private List<ColorChangerMark> _marks = new();

    [Inject]
    private void Init(TeamColorDatabase teamColorDatabase)
    {
        _database = teamColorDatabase;
        FindMarkedObjects();
    }

    public void Recolor(Team team)
    {
        Material material = _database.GetMaterialByTeamType(team.Type);

        foreach (var mark in _marks)
            mark.SetMaterial(material);
    }

    private void FindMarkedObjects()
    {
        _marks = gameObject.GetComponentsInChildren<ColorChangerMark>().ToList();
    }
}
