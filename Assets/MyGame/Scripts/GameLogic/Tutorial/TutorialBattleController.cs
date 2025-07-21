using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Base.GameLogic.Tutorial
{
    public class TutorialBattleController : MonoBehaviour
    {
        [SerializeField] private TutorialTargetArrowDrawer _tutorialTargetArrowDrawer;
        [SerializeField] private ControlPoint _controlPoint;
        [SerializeField] private ButtonClickHandler _launchMingameButton;

        private List<ITutorialAction> _actions = new();
        private InputService _inputService;

        private int _actionIndex = 0;
        private ITutorialAction _currentAction;
        private Player _player;

        private void Update()
        {
            if (_currentAction == null)
                return;

            if (_actions[_actionIndex].IsCompleted())
            {
                Debug.LogWarning($"ACTION COMPLETED");
                
                ChooseNextAction();
            }
        }

        public void Init(Player player, InputService inputService, ControlPoint controlPoint)
        {
            _player = player;
            _inputService = inputService;
            _controlPoint = controlPoint;

            ITutorialAction selectSoldierAction = new SelectSoldierTutorialAction(_player.SoldierCommandController);
            _actions.Add(selectSoldierAction);

            ITutorialAction captureControlPointAction = new CaptureControlPointTutorialAction(_controlPoint);
            _actions.Add(captureControlPointAction);

            ITutorialAction launchMinigameAction = new LaunchMinigameTutorialAction(_launchMingameButton);
            _actions.Add(launchMinigameAction);

            _currentAction = _actions[_actionIndex];
        }

        public void Enable()
        {
            _player.Enable();
            _player.SoldiersSpawned += OnSoldierSpawned;

            //_inputService.Game.Select.performed += OnActionPerformed;
        }

        public void Disable()
        {
            _player.Disable();
        }

        private void ChooseNextAction()
        {
            if (_actionIndex < _actions.Count - 1)
            {
                _actionIndex++;
                Debug.LogWarning($"NEXT ACTION");
                _tutorialTargetArrowDrawer.DrawAbove(_actions[_actionIndex].GetTargetForArrow());
            }
            else
            {
                Debug.LogWarning($"TUTORIAL COMPLETED");
                _tutorialTargetArrowDrawer.HideArrow();
                _currentAction = null;
            }
        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {
            if (_currentAction.IsCompleted())
                Debug.LogWarning($"ACTION COMPLETED");
        }

        private void OnSoldierSpawned(SoldierModel soldierModel)
        {
            _tutorialTargetArrowDrawer.DrawAbove(soldierModel.GetTransform());
            _player.StopSpawningSoldiers();
        }
    }

    public interface ITutorialAction
    {
        bool IsCompleted();

        Transform GetTargetForArrow();
    }

    public class SelectSoldierTutorialAction : ITutorialAction
    {
        private readonly SoldierCommandController _soldierSelector;

        private bool _completed = false;
        private Transform _target;

        public SelectSoldierTutorialAction(SoldierCommandController soldierCommandController)
        {
            _soldierSelector = soldierCommandController;
            _soldierSelector.SoldiersSelected += OnSoldierSelected;
        }

        public Transform GetTargetForArrow() => _target;

        public bool IsCompleted() => _completed;

        private void OnSoldierSelected(List<SoldierModel> soldiers)
        {
            _target = soldiers[0].GetTransform();
            _completed = true;
        }
    }

    public class CaptureControlPointTutorialAction : ITutorialAction
    {
        private readonly ControlPoint _controlPoint;

        private bool _completed = false;
        private Transform _target;

        public CaptureControlPointTutorialAction(ControlPoint controlPoint)
        {
            _controlPoint = controlPoint;
            _controlPoint.Captured += OnCaptured;
            _target = controlPoint.transform;
        }

        public Transform GetTargetForArrow() => _target;

        public bool IsCompleted() => _completed;

        private void OnCaptured(ControlPoint controlPoint)
        {
            _completed = true;
            Debug.LogWarning("ControlPoint Captured");
        }
    }

    public class LaunchMinigameTutorialAction : ITutorialAction
    {
        private readonly ButtonClickHandler _launchMinigameButton;

        private bool _completed = false;

        public LaunchMinigameTutorialAction(ButtonClickHandler launchMinigameButton)
        {
            _launchMinigameButton = launchMinigameButton;

            _launchMinigameButton.Clicked += OnButtonClicked;
        }

        public Transform GetTargetForArrow() => _launchMinigameButton.transform;

        public bool IsCompleted() => _completed;

        private void OnButtonClicked()
        {
            _completed = true;
        }
    }
}
