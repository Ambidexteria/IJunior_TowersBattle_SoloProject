using System.Collections.Generic;
using UnityEngine;

public class TeamColorChanger
{
    private readonly Material _playerColor;
    private readonly Material _npcColor;
    private readonly Material _defaultColor;

    public TeamColorChanger(Material playerColor, Material NPCColor, Material defaultColor)
    {
        _playerColor = playerColor;
        _npcColor = NPCColor;
        _defaultColor = defaultColor;
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
