using System.Collections.Generic;
using UnityEngine;

namespace Base.GameLogic.Tutorial
{
    public class SelectSoldierTutorialAction : ITutorialAction
    {
        private readonly TutorialBattleController _tutorialBattleController;
        private readonly global::Player _player;
        private Transform _target;

        public SelectSoldierTutorialAction(global::Player player, TutorialBattleController tutorialBattleController)
        {
            _tutorialBattleController = tutorialBattleController;
            _player = player;
        }

        public void Enable()
        {
            _tutorialBattleController.PlaceArrow(_target);
            _player.SoldierSelector.SoldiersSelected += OnSoldierSelected;
        }

        public void Disable()
        {
            _tutorialBattleController.HideArrow();
            _player.SoldierSelector.SoldiersSelected -= OnSoldierSelected;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void OnSoldierSelected(List<SoldierModel> soldiers)
        {
            _tutorialBattleController.SetNextAction<CaptureControlPointTutorialAction>();
        }
    }
}
