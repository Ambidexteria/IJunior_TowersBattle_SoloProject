using Base.Data.Game;
using Base.GameLogic.Cannon;
using Base.Services.SaveLoad;
using Base.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.GameLogic.Tutorial
{
    public class TutorialBattleController : MonoBehaviour
    {
        [SerializeField] private Transform _shootMinigamePressRangeTarget;
        [SerializeField] private Transform _playerEnergyBarTarget;
        [SerializeField] private TutorialArrow _tutorialArrow;
        [SerializeField] private ScalingUIFrame _scalingUIFrame;
        [SerializeField] private ButtonClickHandler _launchMingameButton;

        private ControlPoint _controlPoint;
        private Player _player;
        private GameSettings _gameSettings;
        private ISaveLoadService _saveLoadService;

        private Dictionary<Type, ITutorialAction> _actionsDictionary;
        private ITutorialAction _currentAction;
        private bool _enabled;

        public void Init(Player player, ControlPointDatabase controlPointDatabase, GameSettings gameSettings, ISaveLoadService saveLoadService)
        {
            _player = player;
            _controlPoint = controlPointDatabase.GetClosestControlPointToPlayer();
            _gameSettings = gameSettings;
            _saveLoadService = saveLoadService;

            _actionsDictionary = new Dictionary<Type, ITutorialAction>
            {
                { typeof(SelectSoldierTutorialAction), new SelectSoldierTutorialAction(player, this) },
                { typeof(CaptureControlPointTutorialAction), new CaptureControlPointTutorialAction(_controlPoint, _player.Team, this) },
                { typeof(LaunchMinigameTutorialAction), new LaunchMinigameTutorialAction(_launchMingameButton, this) },
                { typeof(PressRangeMinigameTutorialAction), new PressRangeMinigameTutorialAction(player, _shootMinigamePressRangeTarget, this) },
            };
        }

        public void Enable()
        {
            if (_enabled)
                return;

            _player.SoldiersSpawned += OnPlayerSoldierSpawned;
            _enabled = true;
        }

        public void Disable()
        {
            if (_enabled == false)
                return;

            _player.SoldiersSpawned -= OnPlayerSoldierSpawned;
            _tutorialArrow.Hide();
            _scalingUIFrame.Hide();

            _enabled = false;
        }

        public void SetNextAction<TutorialAction>() where TutorialAction : ITutorialAction
        {
            if (_enabled == false)
                return;

            _currentAction?.Disable();

            _currentAction = _actionsDictionary[typeof(TutorialAction)];
            _currentAction.Enable();
        }

        public void EndTutorial()
        {
            _enabled = false;
            _gameSettings.TutorialEnabled = false;
            //_saveLoadService.SaveProgress();
            _currentAction?.Disable();
        }

        public void PlaceArrow(Transform target)
        {
            _tutorialArrow.PlaceAbove(target);
        }

        public void HideArrow()
        {
            _tutorialArrow.Hide();
        }

        public void PlaceFrame(Transform target)
        {
            _scalingUIFrame.PlaceAbove(target);
        }

        public void HideFrame()
        {
            _scalingUIFrame.Hide();
        }

        private void OnPlayerSoldierSpawned(SoldierModel soldierModel)
        {
            _currentAction = _actionsDictionary[typeof(SelectSoldierTutorialAction)];
            _currentAction.SetTarget(soldierModel.GetTransform());
            _currentAction.Enable();

            _player.SoldiersSpawned -= OnPlayerSoldierSpawned;
        }
    }

    public interface ITutorialAction
    {
        void Enable();

        void Disable();

        void SetTarget(Transform target);
    }

    public class SelectSoldierTutorialAction : ITutorialAction
    {
        private readonly TutorialBattleController _tutorialBattleController;
        private readonly Player _player;
        private Transform _target;

        public SelectSoldierTutorialAction(Player player, TutorialBattleController tutorialBattleController)
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

    public class WaitForEnergyBarFilledTutorialAction : ITutorialAction
    {
        private readonly CannonEnergyBarModel _energyBar;
        private readonly TutorialBattleController _tutorialBattleController;
        private bool _completed = false;
        private Transform _target;

        public WaitForEnergyBarFilledTutorialAction(CannonEnergyBarModel energyBar, Transform target, TutorialBattleController tutorialBattleController)
        {
            _energyBar = energyBar;
            _target = target;
            _tutorialBattleController = tutorialBattleController;
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Enable()
        {
            _energyBar.Filled += OnFilled;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void OnFilled()
        {
            _tutorialBattleController.SetNextAction<LaunchMinigameTutorialAction>();
        }
    }

    public class LaunchMinigameTutorialAction : ITutorialAction
    {
        private readonly ButtonClickHandler _launchMinigameButton;
        private readonly TutorialBattleController _tutorialBattleController;

        public LaunchMinigameTutorialAction(ButtonClickHandler launchMinigameButton, TutorialBattleController tutorialBattleController)
        {
            _launchMinigameButton = launchMinigameButton;
            _tutorialBattleController = tutorialBattleController;
        }

        public void Enable()
        {
            _tutorialBattleController.PlaceFrame(_launchMinigameButton.transform);
            _launchMinigameButton.Clicked += OnButtonClicked;
        }

        public void Disable()
        {
            _tutorialBattleController.HideFrame();
            _launchMinigameButton.Clicked -= OnButtonClicked;
        }

        public void SetTarget(Transform target)
        {
        }

        private void OnButtonClicked()
        {
            _tutorialBattleController.EndTutorial();
            //_tutorialBattleController.SetNextAction<PressRangeMinigameTutorialAction>();
        }
    }

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
