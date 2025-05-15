using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TeamColorChanger
{
    private Material _playerColor;
    private Material _npcColor;
    private Material _defaultColor;

    private TeamColorDatabase _database;

    public TeamColorChanger(Material playerColor, Material NPCColor, Material defaultColor)
    {
        _playerColor = playerColor ?? throw new NullReferenceException(nameof(playerColor));
        _npcColor = NPCColor ?? throw new NullReferenceException(nameof(NPCColor));
        _defaultColor = defaultColor ?? throw new NullReferenceException(nameof(defaultColor));
    }

    public void Recolor(Team team, List<ColorChangerMark> marks)
    {
        Material material = GetMaterialByTeamType(team.Type);

        foreach (var mark in marks)
            mark.SetMaterial(material);
    }

    public void Recolor(Team team, ColorChangerMark mark)
    {
        Material material = GetMaterialByTeamType(team.Type);
        mark.SetMaterial(material);
    }

    private Material GetMaterialByTeamType(TeamType teamType)
    {
        if (teamType == TeamType.Player)
            return _playerColor;
        else if (teamType == TeamType.NPC)
            return _npcColor;
        else
            return _defaultColor;
    }
}
