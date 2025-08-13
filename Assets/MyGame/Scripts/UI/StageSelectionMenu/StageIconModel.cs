using System;

namespace Base.UI.StageSelection
{
    public class StageIconModel
    {
        private readonly string _name;

        public StageIconModel(string name)
        {
            _name = name;
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
