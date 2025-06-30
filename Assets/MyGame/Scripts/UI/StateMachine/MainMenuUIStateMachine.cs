using System;
using System.Collections.Generic;

namespace Base.UI.StateMachine
{
    public class MainMenuUIStateMachine
    {
        private readonly Dictionary<Type, IUIState> _states;

        private IUIState _activeState;

        public MainMenuUIStateMachine(UIWindowController mainMenuWondow, UIWindowController shopWindow,
            UIWindowController stagesWindow, UIWindowController settingsWindow,
            UIWindowController leaderboardWindow, UIWindowController authorizationWindow)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(MainMenuUIStateMachine), mainMenuWondow, shopWindow, stagesWindow, 
                settingsWindow, leaderboardWindow, authorizationWindow);

            _states = new Dictionary<Type, IUIState>
            {
                { typeof(MainMenuState), new MainMenuState(mainMenuWondow) },
                { typeof(ShopWindowState), new ShopWindowState(shopWindow) },
                { typeof(StagesWindowState), new StagesWindowState(stagesWindow) },
                { typeof(SettingsMenuState), new SettingsMenuState(settingsWindow) },
                { typeof(LeaderboardWindowState), new LeaderboardWindowState(leaderboardWindow) },
                { typeof(AutorizationWindowState), new AutorizationWindowState(authorizationWindow) },
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
