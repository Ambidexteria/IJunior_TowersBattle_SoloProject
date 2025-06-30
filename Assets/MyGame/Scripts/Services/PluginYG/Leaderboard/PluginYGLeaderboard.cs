using Base.Data.Game;
using YG;

namespace Base.Services.PluginYG.LeaderBoard
{
    public class PluginYGLeaderboard
    {
        private const string BoardName = "TEST";

        private readonly PlayerScore _playerScore;

        public PluginYGLeaderboard(PlayerScore playerScore)
        {
            _playerScore = playerScore;
        }

        public void UpdateScore()
        {
            YG2.SetLeaderboard(BoardName, _playerScore.Value);
        }
    }
}
