using Base.Infrastructure;

namespace Base.UI.PauseMenu
{
    public class PauseMenuModel
    {
        private readonly Game _game;

        public PauseMenuModel(Game game)
        {
            _game = game;
        }

        public void RestartLevel()
        {
            _game.LoadGameScene();
        }

        public void ReturnToMainMenu()
        {
            _game.LoadMainMenu();
        }
    }
}
