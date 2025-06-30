using Base.GameLogic.Cannon;
using System;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameModel
    {
        private readonly ShootMinigameLauncherModel _shootMinigameLauncher;
        private readonly CannonEnergyBarModel _cannonEnergyBar;
        private readonly ShootMinigamePressRangeModel _minigamePressRange;
        private bool _enabled = false;
        private bool _minigameStarted = false;

        public ShootMinigameModel(CannonEnergyBarModel cannonEnergyBar, ShootMinigameLauncherModel launchMinigameModel, 
            ShootMinigamePressRangeModel shootMinigamePressRangeModel)
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShootMinigameModel), ExceptionsTest.ConstructorName, cannonEnergyBar, launchMinigameModel, 
                shootMinigamePressRangeModel);

            _minigamePressRange = shootMinigamePressRangeModel;
            _cannonEnergyBar = cannonEnergyBar;
            _shootMinigameLauncher = launchMinigameModel;
        }

        public event Action ReadyForShoot;
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

            _shootMinigameLauncher.Disable();
            _minigamePressRange.Disable();

            _enabled = false;
        }

        public void LaunchMinigame()
        {
            if(_minigameStarted)
                return;

            _minigamePressRange.Enable();
            _minigameStarted = true;
        }

        public void EndMinigame()
        {
            if (_minigameStarted == false)
                return;

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
            ReadyForShoot?.Invoke();
            _shootMinigameLauncher.Enable();
        }
    }
}
