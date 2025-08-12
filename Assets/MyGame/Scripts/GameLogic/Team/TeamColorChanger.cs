using System.Collections.Generic;
using UnityEngine;

public class TeamColorChanger
{
    private Material _playerColor;
    private Material _npcColor;
    private Material _defaultColor;

    public TeamColorChanger(Material playerColor, Material NPCColor, Material defaultColor)
    {
        ExceptionsTest.NullRefConstructorTest(nameof(TeamColorChanger), playerColor, NPCColor, defaultColor);

        _playerColor = playerColor;
        _npcColor = NPCColor;
        _defaultColor = defaultColor;
    }

    public void Recolor(Team team, List<ColorChangerMark> marks)
    {
        ExceptionsTest.NullRefMethodTest(nameof(TeamColorChanger), nameof(Recolor), team, marks);
        ExceptionsTest.EmptyListTest(nameof(TeamColorChanger), nameof(Recolor), marks);

        Material material = GetMaterialByTeamType(team.Type);

        foreach (var mark in marks)
            mark.SetMaterial(material);
    }

    public void Recolor(Team team, ColorChangerMark mark)
    {
        ExceptionsTest.NullRefMethodTest(nameof(TeamColorChanger), nameof(Recolor), team, mark);

        Material material = GetMaterialByTeamType(team.Type);
        mark.SetMaterial(material);
    }

    public Color GetColor(Team team)
    {
        ExceptionsTest.NullRefMethodTest(nameof(TeamColorChanger), nameof(GetColor), team);

        Material material = GetMaterialByTeamType(team.Type);
        return material.color;
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
