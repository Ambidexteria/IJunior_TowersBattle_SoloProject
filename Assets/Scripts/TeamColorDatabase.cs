using System;
using UnityEngine;
using Zenject;

public class TeamColorDatabase
{
    private Material _playerColor;
    private Material _npcColor;
    private Material _defaultColor;

    [Inject]
    public TeamColorDatabase(Material playerColor, Material NPCColor, Material defaultColor) 
    { 
        _playerColor = playerColor ?? throw new NullReferenceException(nameof(playerColor));
        _npcColor = NPCColor ?? throw new NullReferenceException(nameof(NPCColor));
        _defaultColor = defaultColor ?? throw new NullReferenceException(nameof(defaultColor));
    }

    public Material GetMaterialByTeamType(TeamType teamType)
    {
        if(teamType == TeamType.Player)
            return _playerColor;
        else if(teamType == TeamType.NPC)
            return _npcColor;
        else 
            return _defaultColor;
    }
}
