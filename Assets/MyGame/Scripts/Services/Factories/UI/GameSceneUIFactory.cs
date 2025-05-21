using Base.UI.Game.StateMachine;
using UnityEngine;

namespace Base.Services.Factories.UI
{
    public class GameSceneUIFactory : MonoBehaviour
    {
        [SerializeField] private UIWindowController _cannonsHUD;
        [SerializeField] private UIWindowController _playerCannonHUD;
        [SerializeField] private UIWindowController _npcCannonHUD;
        [SerializeField] private UIWindowController _shootMinigameUI;
        [SerializeField] private UIWindowController _pauseWindowUI;
        [SerializeField] private UIWindowController _winMessage;
        [SerializeField] private UIWindowController _defeatMessage;

        private GameUIStateMachine _uiStateMachine;

        public GameUIStateMachine GetUIStateMachine()
        {
            if (_uiStateMachine == null)
            {

                _uiStateMachine = new GameUIStateMachine(_cannonsHUD, _shootMinigameUI,
                    _pauseWindowUI, _winMessage, _defeatMessage);

                _uiStateMachine.Enter<CannonsHUDState>();
            }

            return _uiStateMachine;
        }
    }
}
