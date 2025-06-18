using System;

namespace Base.UI.StageSelection
{
    public class StageIconModel
    {
        private readonly string _name;
        private bool _unlocked;

        public StageIconModel(string name, bool unlocked)
        {
            _name = name;
            _unlocked = unlocked;
        }

        public string Name => _name;

        public event Action<string> Choosed;
        public event Action BorderEnabled;
        public event Action BorderDisabled;

        public void Choose()
        {
            Choosed?.Invoke(_name);
        }

        public void ShowBorder()
        {
            BorderEnabled?.Invoke();
        }

        public void HideBorder()
        {
            BorderDisabled?.Invoke();
        }
    }
}
