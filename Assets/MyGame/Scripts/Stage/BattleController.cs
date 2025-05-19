using Base.UI.Game.StateMachine;
using UnityEngine;

public class BattleController
{
    private readonly Player _player;
    private readonly NPC _npc;
    private readonly GameUIStateMachine _uIStateMachine;

    public BattleController(Player player, NPC npc, GameUIStateMachine uIStateMachine)
    {
        _player = player;
        _npc = npc;
        _uIStateMachine = uIStateMachine;
    }

    public void Enable()
    {
        _player.Enable();
        _npc.Enable();

        _player.Defeated += OnPlayerDefeated;
        _npc.Defeated += OnNPCDefeated;
    }

    public void Disable()
    {
        _player.Disable();
        _npc.Disable();

        _player.Defeated -= OnPlayerDefeated;
        _npc.Defeated -= OnNPCDefeated;
    }

    private void OnPlayerDefeated()
    {
        _uIStateMachine.Enter<DefeatMessageState>();

        _player.Disable();
        _npc.Disable();
    }

    private void OnNPCDefeated()
    {
        _uIStateMachine.Enter<WinMessageState>();

        _player.Disable();
        _npc.Disable();
    }
}
