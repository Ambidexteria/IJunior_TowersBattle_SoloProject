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
            UIWindowController winMessage,
            UIWindowController restoreHealthForRewardAds)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(GameUIStateMachine), cannonsHUD, shootMinigame, pauseWindow,
                settingsWindow, winMessage);

            _states = new Dictionary<Type, IUIState>
            {
                { typeof(CannonsHUDState), new CannonsHUDState(cannonsHUD) },
                { typeof(ShootMinigameState), new ShootMinigameState(shootMinigame) },
                { typeof(PauseState), new PauseState(pauseWindow) },
                { typeof(SettingsMenuState), new SettingsMenuState(settingsWindow) },
                { typeof(BattleEndState), new BattleEndState(winMessage) },
                { typeof(RestoreHealthForRewardAdsWindow), new RestoreHealthForRewardAdsWindow(restoreHealthForRewardAds) },
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
