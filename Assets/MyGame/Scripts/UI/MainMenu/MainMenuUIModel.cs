using System;

namespace Base.UI.MainMenu
{
    public class MainMenuUIModel
    {
        public event Action Enabled;
        public event Action Disabled;
        public event Action StartingBattle;

        public void StartBattle()
        {
            StartingBattle?.Invoke();
        }

        public void Enable()
        {
            Enabled?.Invoke();
        }

        public void Disable()
        {
            Disabled?.Invoke();
        }
    }
}
