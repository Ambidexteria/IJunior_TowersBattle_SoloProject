using Base.GameLogic.Cannon;
using System;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameModel
    {
        private readonly ShootMinigameLauncherModel _shootMinigameLauncher;
        private readonly CannonEnergyBar _cannonEnergyBar;
        private readonly TimeController _timeController;
        private readonly ShootMinigamePressRangeModel _minigamePressRange;
        private bool _enabled = false;
        private bool _minigameStarted = false;

        public ShootMinigameModel(CannonEnergyBar cannonEnergyBar, ShootMinigameLauncherModel launchMinigameModel, 
            ShootMinigamePressRangeModel shootMinigamePressRangeModel, TimeController timeController)
        {
            _timeController = timeController;

            _minigamePressRange = shootMinigamePressRangeModel;
            _cannonEnergyBar = cannonEnergyBar;
            _shootMinigameLauncher = launchMinigameModel;
        }

        public bool Activated => _enabled;

        public event Action Winned;
        public event Action Loosed;

        public void Enable()
        {
            if(_enabled) 
                return;

            _cannonEnergyBar.Filled += OnEnergyFilled;
            _enabled = true;
        }

        public void Disable()
        {
            if(_enabled == false)
                return;

            _cannonEnergyBar.Filled -= OnEnergyFilled;
            _enabled = false;
        }

        public void LaunchMinigame()
        {
            if(_minigameStarted)
                return;

            _timeController.SetSlowMotionTimeScale();
            _minigamePressRange.Enable();

            _minigameStarted = true;
        }

        public void EndMinigame()
        {
            if (_minigameStarted == false)
                return;

            _timeController.SetDefaultTimeScale();

            if (_minigamePressRange.IsCurrentValueInPressRange())
            {
                Winned?.Invoke();
            }
            else
            {
                Loosed?.Invoke();
            }

            _minigamePressRange.Disable();
            _shootMinigameLauncher.Disable();

            _minigameStarted = false;
        }
        private void OnEnergyFilled()
        {
            _shootMinigameLauncher.Enable();
        }
    }
}
