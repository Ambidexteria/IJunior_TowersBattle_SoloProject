namespace Base.UI.StateMachine
{
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
}
