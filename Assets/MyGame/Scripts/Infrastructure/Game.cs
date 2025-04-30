namespace Base.Infrastructure
{
    public class Game
    {
        private GameStateMachine _gameStateMachine;

        public GameStateMachine GameStateMachine => _gameStateMachine;

        public Game(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
    }
}