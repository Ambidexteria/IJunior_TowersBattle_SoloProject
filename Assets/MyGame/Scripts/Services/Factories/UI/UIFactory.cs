using Base.Services.AssetManagment;
using Base.UI.MainMenu;
using System;
using UnityEngine;

namespace Base.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private const string UIFolder = "UI/MainMenuUI";
        private readonly AssetLoader _assetloader;

        public event Action<GameObject> Created;

        public UIFactory(AssetLoader assetloader)
        {
            _assetloader = assetloader;
        }

        public void CreateUI(string name)
        {
            GameObject uiGameobject = _assetloader.Instantiate(name);
            Created?.Invoke(uiGameobject);
        }

        public MainMenuUIModel CreateMainMenuModel()
        {
            return _assetloader.Instantiate<MainMenuUISetup>(UIFolder).GetModel();
        }
    }
}
