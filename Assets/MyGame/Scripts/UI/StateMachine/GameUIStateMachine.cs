using System;
using System.Collections.Generic;

namespace Base.UI.StateMachine
{
    public class GameUIStateMachine
    {
        private readonly Dictionary<Type, IUIState> _states;

        private IUIState _activeState;

        public GameUIStateMachine(UIWindowController cannonsHUD,
            UIWindowController shootMinigame, 
            UIWindowController pauseWindow,  
            UIWindowController settingsWindow,
            UIWindowController winMessage, UIWindowController defeatMessage)
        {
            _states = new Dictionary<Type, IUIState>
            {
                { typeof(CannonsHUDState), new CannonsHUDState(cannonsHUD) },
                { typeof(ShootMinigameState), new ShootMinigameState(shootMinigame) },
                { typeof(PauseState), new PauseState(pauseWindow) },
                { typeof(SettingsMenuState), new SettingsMenuState(settingsWindow) },
                { typeof(WinMessageState), new WinMessageState(winMessage) },
                { typeof(DefeatMessageState), new DefeatMessageState(defeatMessage, pauseWindow) }
            };
        }

        public void Enter<TState>() where TState : IUIState
        {
            _activeState?.Exit();

            _activeState = _states[typeof(TState)];
            _activeState?.Enter();
        }
    }
}
