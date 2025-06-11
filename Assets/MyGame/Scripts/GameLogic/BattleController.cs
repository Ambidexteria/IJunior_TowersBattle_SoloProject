using Base.UI.StateMachine;

namespace Base.GameLogic
{
    public class BattleController
    {
        private readonly Player _player;
        private readonly NPC _npc;
        private readonly GameUIStateMachine _uIStateMachine;
        private readonly BattleEndModel _battleEnd;

        public BattleController(Player player, NPC npc, GameUIStateMachine uIStateMachine, BattleEndModel battleEnd)
        {
            _player = player;
            _npc = npc;
            _uIStateMachine = uIStateMachine;
            _battleEnd = battleEnd;
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
            _battleEnd.End(false, _npc.CannonDamageTaken);
            _uIStateMachine.Enter<DefeatMessageState>();

            _player.Disable();
            _npc.Disable();
        }

        private void OnNPCDefeated()
        {
            _battleEnd.End(true, _npc.CannonDamageTaken);
            _uIStateMachine.Enter<WinMessageState>();

            _player.Disable();
            _npc.Disable();
        }
    }
}
