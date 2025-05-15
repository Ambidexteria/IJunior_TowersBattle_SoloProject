using Base.UI.MainMenu;
using System;
using UnityEngine;

namespace Base.Services.Factories.UI
{
    public interface IUIFactory
    {
        event Action<GameObject> Created;

        MainMenuUIModel CreateMainMenuModel();
        void CreateUI(string name);
    }
}