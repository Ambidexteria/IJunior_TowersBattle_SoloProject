using Base.Data.Game;
using Base.GameLogic.Cannon;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.GameLogic.Tutorial
{
    public class TutorialBattleController : MonoBehaviour
    {
        [SerializeField] private Transform _shootMinigamePressRangeTarget;
        [SerializeField] private Transform _playerEnergyBarTarget;
        [SerializeField] private TutorialTargetArrowDrawer _tutorialTargetArrowDrawer;
        [SerializeField] private ButtonClickHandler _launchMingameButton;

        private ControlPointDatabase _controlPointDatabase;
        private ControlPoint _controlPoint;
        private Player _player;
        private NPC _npc;
        private GameSettings _gameSettings;

        private Dictionary<Type, ITutorialAction> _actionsDicitionary;
        private bool _enabled;

        public void Init(Player player, NPC npc, ControlPointDatabase controlPointDatabase, GameSettings gameSettings)
        {
            _player = player;
            _npc = npc;
            _controlPoint = controlPointDatabase.GetClosestControlPointToPlayer();
            _gameSettings = gameSettings;

            _actionsDicitionary = new Dictionary<Type, ITutorialAction>
            {
                { typeof(SelectSoldierTutorialAction), new SelectSoldierTutorialAction(player, this) },
                { typeof(CaptureControlPointTutorialAction), new CaptureControlPointTutorialAction(_controlPoint, _player.Team, this) },
                { typeof(WaitForEnergyBarFilledTutorialAction), new WaitForEnergyBarFilledTutorialAction(player.CannonEnergyBar, _playerEnergyBarTarget, this) },
                { typeof(LaunchMinigameTutorialAction), new LaunchMinigameTutorialAction(_launchMingameButton,this) },
                { typeof(PressRangeMinigameTutorialAction), new PressRangeMinigameTutorialAction(player, _shootMinigamePressRangeTarget, this) },
            };
        }

        public void Enable()
        {
            if (_enabled)
                return;

            _player.Enable();
            _player.SoldiersSpawned += OnPlayerSoldierSpawned;

            _npc.Enable();
            _npc.SoldierSpawned += OnNPCSoldierSpawned;
            _npc.CannonShooted += OnNPCCannonShooted;
            _npc.Defeated += OnNPCDefeated;

            _enabled = true;
        }

        public void Disable()
        {
            if (_enabled == false)
                return;

            _player.Disable();
            _npc.Disable();
            _tutorialTargetArrowDrawer.HideArrow();

            _enabled = false;
        }

        public void SetNextAction<TutorialAction>() where TutorialAction : ITutorialAction
        {
            if(_enabled == false)
                return;

            ITutorialAction action = _actionsDicitionary[typeof(TutorialAction)];

            if (action.IsCompleted())
                return;

            _tutorialTargetArrowDrawer.DrawAbove(action.GetTargetForArrow());
        }

        public void EndTutorial()
        {
            _enabled = false;
            _gameSettings.TutorialEnabled = false;
            _tutorialTargetArrowDrawer.HideArrow();
        }

        private void OnPlayerSoldierSpawned(SoldierModel soldierModel)
        {
            //_player.StopSpawningSoldiers();

            SetNextAction<SelectSoldierTutorialAction>();
        }

        private void OnNPCSoldierSpawned()
        {
            //_npc.StopSpawningSoldiers();
        }

        private void OnNPCCannonShooted()
        {
            //_npc.DisableCannon();
        }

        private void OnNPCDefeated()
        {
            //_npc.Disable();
            //_player.Disable();
            //_uIStateMachine.Enter<TutorialEndWindowState>();
            //_gameSettings.TutorialEnabled = false;
        }
    }

    public interface ITutorialAction
    {
        bool IsCompleted();

        Transform GetTargetForArrow();
    }

    public class SelectSoldierTutorialAction : ITutorialAction
    {
        private readonly SoldierSelector _soldierSelector;
        private readonly TutorialBattleController _tutorialBattleController;
        private bool _completed = false;
        private Transform _target;

        public SelectSoldierTutorialAction(Player player, TutorialBattleController tutorialBattleController)
        {
            _soldierSelector = player.SoldierSelector;
            _tutorialBattleController = tutorialBattleController;
            _soldierSelector.SoldiersSelected += OnSoldierSelected;
            player.SoldiersSpawned += OnSoldierSpawned;
        }

        public Transform GetTargetForArrow() => _target;

        public bool IsCompleted() => _completed;

        private void OnSoldierSpawned(SoldierModel soldier)
        {
            _target = soldier.GetTransform();
        }

        private void OnSoldierSelected(List<SoldierModel> soldiers)
        {
            _tutorialBattleController.SetNextAction<CaptureControlPointTutorialAction>();
            _completed = true;
        }
    }

    public class CaptureControlPointTutorialAction : ITutorialAction
    {
        private readonly ControlPoint _controlPoint;
        private readonly Team _team;
        private readonly TutorialBattleController _tutorialBattleController;

        private bool _completed = false;
        private Transform _target;

        public CaptureControlPointTutorialAction(ControlPoint controlPoint, Team team, TutorialBattleController tutorialBattleController)
        {
            _controlPoint = controlPoint;
            _team = team;
            _tutorialBattleController = tutorialBattleController;
            _controlPoint.Captured += OnCaptured;
            _target = controlPoint.transform;
        }

        public Transform GetTargetForArrow() => _target;

        public bool IsCompleted() => _completed;

        private void OnCaptured(ControlPoint controlPoint)
        {
            if (controlPoint.Team == _team.Type)
            {
                _tutorialBattleController.SetNextAction<WaitForEnergyBarFilledTutorialAction>();
                _completed = true;
            }
        }
    }

    public class WaitForEnergyBarFilledTutorialAction : ITutorialAction
    {
        private readonly CannonEnergyBarModel _energyBar;
        private readonly Transform _target;
        private readonly TutorialBattleController _tutorialBattleController;
        private bool _completed = false;

        public WaitForEnergyBarFilledTutorialAction(CannonEnergyBarModel energyBar, Transform target, TutorialBattleController tutorialBattleController)
        {
            _energyBar = energyBar;
            _target = target;
            _tutorialBattleController = tutorialBattleController;

            _energyBar.Filled += OnFilled;
        }

        public Transform GetTargetForArrow() => _target;

        public bool IsCompleted() => _completed;

        private void OnFilled()
        {
            _tutorialBattleController.SetNextAction<LaunchMinigameTutorialAction>();
        }
    }

    public class LaunchMinigameTutorialAction : ITutorialAction
    {
        private readonly ButtonClickHandler _launchMinigameButton;
        private readonly TutorialBattleController _tutorialBattleController;
        private bool _completed = false;

        public LaunchMinigameTutorialAction(ButtonClickHandler launchMinigameButton, TutorialBattleController tutorialBattleController)
        {
            _launchMinigameButton = launchMinigameButton;
            _tutorialBattleController = tutorialBattleController;

            _launchMinigameButton.Clicked += OnButtonClicked;
        }

        public Transform GetTargetForArrow() => _launchMinigameButton.transform;

        public bool IsCompleted() => _completed;

        private void OnButtonClicked()
        {
            _tutorialBattleController.SetNextAction<PressRangeMinigameTutorialAction>();
        }
    }

    public class PressRangeMinigameTutorialAction : ITutorialAction
    {
        private Player _player;
        private Transform _target;
        private TutorialBattleController _tutorialBattleController;
        private bool _completed = false;

        public PressRangeMinigameTutorialAction(Player player, Transform target, TutorialBattleController tutorialBattleController)
        {
            _player = player;
            _target = target;
            _tutorialBattleController = tutorialBattleController;

            _player.ShooMinigameWinned += OnMinigameWinned;
        }

        public Transform GetTargetForArrow() => _target;

        public bool IsCompleted() => _completed;

        private void OnMinigameWinned(bool isWinned)
        {
            if (isWinned)
                _tutorialBattleController.EndTutorial();
            else
                _tutorialBattleController.SetNextAction<LaunchMinigameTutorialAction>();
        }
    }
}
