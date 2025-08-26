using UnityEngine;

namespace Base.GameLogic.Tutorial
{
    public class PressRangeMinigameTutorialAction : ITutorialAction
    {
        private readonly Player _player;
        private readonly TutorialBattleController _tutorialBattleController;
        private Transform _target;

        public PressRangeMinigameTutorialAction(Player player, Transform target, TutorialBattleController tutorialBattleController)
        {
            _player = player;
            _target = target;
            _tutorialBattleController = tutorialBattleController;
        }

        public void Enable()
        {
            _tutorialBattleController.PlaceFrame(_target);
            _player.ShooMinigameWinned += OnMinigameWinned;
        }

        public void Disable()
        {
            _tutorialBattleController.HideFrame();
            _player.ShooMinigameWinned -= OnMinigameWinned;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void OnMinigameWinned(bool isWinned)
        {
            if (isWinned)
                _tutorialBattleController.EndTutorial();
            else
                _tutorialBattleController.SetNextAction<LaunchMinigameTutorialAction>();
        }
    }
}
