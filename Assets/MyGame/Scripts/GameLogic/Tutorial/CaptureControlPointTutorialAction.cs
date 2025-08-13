using UnityEngine;

namespace Base.GameLogic.Tutorial
{
    public class CaptureControlPointTutorialAction : ITutorialAction
    {
        private readonly ControlPoint _controlPoint;
        private readonly Team _team;
        private readonly TutorialBattleController _tutorialBattleController;
        private Transform _target;

        public CaptureControlPointTutorialAction(ControlPoint controlPoint, Team team, TutorialBattleController tutorialBattleController)
        {
            _controlPoint = controlPoint;
            _team = team;
            _tutorialBattleController = tutorialBattleController;
            _target = controlPoint.transform;
        }
        public void Enable()
        {
            _tutorialBattleController.PlaceArrow(_target);
            _controlPoint.Captured += OnCaptured;
        }

        public void Disable()
        {
            _tutorialBattleController.HideArrow();
            _controlPoint.Captured -= OnCaptured;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void OnCaptured(ControlPoint controlPoint)
        {
            if (controlPoint.Team == _team.Type)
                _tutorialBattleController.SetNextAction<LaunchMinigameTutorialAction>();
        }
    }
}
