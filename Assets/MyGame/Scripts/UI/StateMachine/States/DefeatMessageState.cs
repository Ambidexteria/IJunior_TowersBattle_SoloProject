namespace Base.UI.StateMachine
{
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
}
