using Base.Services.SceneManagment;
using Base.Services.TimeManagment;

namespace Base.UI.PauseMenu
{
    public class PauseMenuModel
    {
        private readonly TimeController _timeController;
        private readonly SceneChanger _sceneChanger;

        public PauseMenuModel(SceneChanger sceneChanger)
        {
            ExceptionsTest.NullRefConstructorTest(nameof(PauseMenuModel), sceneChanger);

            _sceneChanger = sceneChanger;
        }

        public void RestartLevel()
        {
            _sceneChanger.ReloadGameScene();
        }

        public void ReturnToMainMenu()
        {
            _sceneChanger.ReturnToMainMenu();
        }
    }
}
