using Base.Services.AssetManagment;
using Base.UI.MainMenu;
using UnityEngine;

namespace Base.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private const string UIFolder = "UI/MainMenuUI";
        private readonly AssetLoader _assetloader;

        public UIFactory(AssetLoader assetloader)
        {
            _assetloader = assetloader;
        }

        public MainMenuUIModel CreateMainMenuModel()
        {
            return _assetloader.Instantiate<MainMenuUISetup>(UIFolder).GetModel();
        }
    }
}
