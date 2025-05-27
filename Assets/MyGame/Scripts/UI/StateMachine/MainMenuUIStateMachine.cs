using System;
using System.Collections.Generic;

namespace Base.UI.StateMachine
{
    public class MainMenuUIStateMachine
    {
        private readonly Dictionary<Type, IUIState> _states;

        private IUIState _activeState;

        public MainMenuUIStateMachine(UIWindowController mainMenuWondow, UIWindowController shopWindow,
            UIWindowController stagesWindow, UIWindowController settingsWindow)
        {
            _states = new Dictionary<Type, IUIState>
            {
                { typeof(MainMenuState), new MainMenuState(mainMenuWondow) },
                { typeof(ShopWindowState), new ShopWindowState(shopWindow) },
                { typeof(StagesWindowState), new StagesWindowState(stagesWindow) },
                { typeof(SettingsMenuState), new SettingsMenuState(settingsWindow) }
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
