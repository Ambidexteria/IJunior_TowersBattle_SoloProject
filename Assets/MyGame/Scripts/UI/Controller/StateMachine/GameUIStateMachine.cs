using System;
using System.Collections.Generic;

namespace Base.UI.Game.StateMachine
{
    public class GameUIStateMachine
    {
        private readonly Dictionary<Type, IUIState> _states;

        private IUIState _activeState;

        public GameUIStateMachine(UIWindowController cannonsHUD, UIWindowController shootMinigame, 
            UIWindowController pauseWindow, UIWindowController winMessage, 
            UIWindowController defeatMessage)
        {
            _states = new Dictionary<Type, IUIState>
            {
                { typeof(CannonsHUDState), new CannonsHUDState(cannonsHUD) },
                { typeof(ShootMinigameState), new ShootMinigameState(shootMinigame) },
                { typeof(PauseState), new PauseState(pauseWindow) },
                { typeof(WinMessageState), new WinMessageState(winMessage) },
                { typeof(DefeatMessageState), new DefeatMessageState(defeatMessage) }
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
        private readonly UIWindowController _canvas;

        public UIState(UIWindowController window)
        {
            _canvas = window;
        }

        public virtual void Enter()
        {
            _canvas.gameObject.SetActive(true);
        }

        public virtual void Exit()
        {
            _canvas.gameObject.SetActive(false);
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
        public DefeatMessageState(UIWindowController window) : base(window)
        {
        }
    }

    public interface IUIState
    {
        void Enter();
        void Exit();
    }
}
