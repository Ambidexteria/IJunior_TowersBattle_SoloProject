using Base.Services.TimeManagment;
using Base.UI.RewardForAds;
using Base.UI.StateMachine;

namespace Base.GameLogic
{
    public class BattleController
    {
        private readonly global::Player _player;
        private readonly NPC _npc;
        private readonly GameUIStateMachine _uIStateMachine;
        private readonly BattleEndModel _battleEnd;
        private readonly TimeController _timeController;
        private readonly RestoreHealthForRewardAdsModel _restoreHealthForRewardAds;

        public BattleController(
            global::Player player, 
            NPC npc, 
            GameUIStateMachine uIStateMachine, 
            BattleEndModel battleEnd, 
            TimeController timeController, 
            RestoreHealthForRewardAdsModel restoreHealthForRewardAds)
        {
            _player = player;
            _npc = npc;
            _uIStateMachine = uIStateMachine;
            _battleEnd = battleEnd;
            _timeController = timeController;
            _restoreHealthForRewardAds = restoreHealthForRewardAds;
        }

        public void Enable()
        {
            _player.Enable();
            _npc.Enable();

            _player.Defeated += OnPlayerDefeated;
            _npc.Defeated += OnNPCDefeated;
            _restoreHealthForRewardAds.RewardGained += OnRewardGained;
        }

        public void Disable()
        {
            _player.Disable();
            _npc.Disable();

            _player.Defeated -= OnPlayerDefeated;
            _npc.Defeated -= OnNPCDefeated;
            _restoreHealthForRewardAds.RewardGained -= OnRewardGained;
        }

        private void OnPlayerDefeated()
        {
            _timeController.Pause();
            _uIStateMachine.Enter<RestoreHealthForRewardAdsWindow>();
        }

        private void OnNPCDefeated()
        {
            _battleEnd.End(true, _npc.CannonDamageTaken);
            _uIStateMachine.Enter<BattleEndState>();

            _player.Disable();
            _npc.Disable();
        }

        private void OnRewardGained(bool gained)
        {
            _timeController.Resume();

            if (gained)
            {
                _uIStateMachine.Enter<CannonsHUDState>();
            }
            else
            {
                _battleEnd.End(false, _npc.CannonDamageTaken);
                _uIStateMachine.Enter<BattleEndState>();

                _player.Disable();
                _npc.Disable();
            }
        }
    }
}
