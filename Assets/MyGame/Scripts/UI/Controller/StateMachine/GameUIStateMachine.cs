using System;
using System.Collections.Generic;

namespace Base.UI.Game.StateMachine
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

    public abstract class UIState : IUIState
    {
        private readonly UIWindowController _window;

        protected UIState(UIWindowController window)
        {
            _window = window;
        }

        public virtual void Enter()
        {
            _window.Show();
        }

        public virtual void Exit()
        {
            _window.Hide();
        }
    }

    public class CannonsHUDState : UIState
    {
        public CannonsHUDState(UIWindowController window) : base(window)
        {
        }
    }

    public class ShootMinigameState : UIState
    {
        public ShootMinigameState(UIWindowController window) : base(window)
        {
        }
    }

    public class PauseState : UIState
    {
        public PauseState(UIWindowController window) : base(window)
        {
        }
    }

    public class WinMessageState : UIState
    {
        public WinMessageState(UIWindowController window) : base(window)
        {
        }
    }

    public class DefeatMessageState : UIState
    {
        private readonly UIWindowController _pauseMenu;

        public DefeatMessageState(UIWindowController defeatMessage, UIWindowController pauseMenu) : base(defeatMessage)
        {
            _pauseMenu = pauseMenu;
        }

        public override void Enter()
        {
            base.Enter();
            _pauseMenu.Show();
        }

        public override void Exit()
        {
            base.Exit();
            _pauseMenu.Hide();
        }
    }

    public class SettingsMenuState : UIState
    {
        public SettingsMenuState(UIWindowController window) : base(window)
        {
        }
    }

    public interface IUIState
    {
        void Enter();
        void Exit();
    }
}
