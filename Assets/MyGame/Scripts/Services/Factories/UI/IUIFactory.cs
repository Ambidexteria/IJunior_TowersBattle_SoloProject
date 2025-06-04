using System;
using UnityEngine;

namespace Base.Services.Factories.UI
{
    public interface IUIFactory
    {
        event Action<Canvas> Created;

        void CreateUI(string name);
    }
}