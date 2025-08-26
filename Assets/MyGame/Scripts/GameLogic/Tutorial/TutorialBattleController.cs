using System;
using System.Collections.Generic;
using Base.Data.Game;
using Base.Utils;
using UnityEngine;

namespace Base.GameLogic.Tutorial
{
    public class TutorialBattleController : MonoBehaviour
    {
        [SerializeField] private int _energyIncomeMultiplyer = 5;
        [SerializeField] private Transform _shootMinigamePressRangeTarget;
        [SerializeField] private Transform _playerEnergyBarTarget;
        [SerializeField] private TutorialArrow _tutorialArrow;
        [SerializeField] private ScalingUIFrame _scalingUIFrame;
        [SerializeField] private ButtonClickHandler _launchMingameButton;

        private ControlPoint _controlPoint;
        private Player _player;
        private GameSettings _gameSettings;

        private Dictionary<Type, ITutorialAction> _actionsDictionary;
        private ITutorialAction _currentAction;
        private bool _enabled;

        public void Init(Player player, ControlPointDatabase controlPointDatabase, GameSettings gameSettings)
        {
            _player = player;
            _controlPoint = controlPointDatabase.GetClosestControlPointToPlayer();
            _gameSettings = gameSettings;

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

        public void SetNextAction<TutorialAction>() where TutorialAction 
            : ITutorialAction
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

        public void IncreaseEnergyIncome()
        {
            _player.CannonEnergyBar.MultiplyEnergyIncome(_energyIncomeMultiplyer);
        }

        public void DecreaseEnergyIncome()
        {
            _player.CannonEnergyBar.RestoreDefaultEnergyIncome();
        }

        private void OnPlayerSoldierSpawned(SoldierModel soldierModel)
        {
            _currentAction = _actionsDictionary[typeof(SelectSoldierTutorialAction)];
            _currentAction.SetTarget(soldierModel.GetTransform());
            _currentAction.Enable();

            _player.SoldiersSpawned -= OnPlayerSoldierSpawned;
        }
    }
}
