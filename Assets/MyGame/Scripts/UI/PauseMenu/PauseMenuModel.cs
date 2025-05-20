using Base.Services.TimeManagment;
using Base.UI.Game.StateMachine;
using System;

namespace Base.UI.PauseMenu
{
    public class PauseMenuModel
    {
        private readonly TimeController _timeController;
        private readonly GameUIStateMachine _gameUIStateMachine;

        public PauseMenuModel(TimeController timeController, GameUIStateMachine gameUIStateMachine)
        {
            _timeController = timeController;
            _gameUIStateMachine = gameUIStateMachine;
        }

        public event Action Closed;
        public event Action RestartingLevel;
        public event Action ReturningToMainMenu;

        public void Resume()
        {
            _gameUIStateMachine.Enter<CannonsHUDState>();
            _timeController.Resume();
        }

        public void RestartLevel()
        {
            RestartingLevel?.Invoke();
        }

        public void ReturnToMainMenu()
        {
            ReturningToMainMenu?.Invoke();
        }

        public void ShowSettingsMenu()
        {
            _gameUIStateMachine.Enter<SettingsMenuState>();
        }
    }
}
