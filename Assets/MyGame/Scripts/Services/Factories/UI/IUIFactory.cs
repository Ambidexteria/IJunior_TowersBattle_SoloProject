using Base.UI.MainMenu;

namespace Base.Services.Factories.UI
{
    public interface IUIFactory
    {
        MainMenuUIModel CreateMainMenuModel();
    }
}