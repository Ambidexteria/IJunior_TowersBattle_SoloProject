using Base.Data;
using Base.Data.Game;
using Base.Infrastructure;
using Base.PLayer;
using Base.Services.Audio;
using Base.Services.PluginYG;
using Base.Services.SaveLoad;
using System;

namespace Base.GameLogic
{
    public class BattleEndModel
    {
        private readonly Game _game;
        private readonly Wallet _wallet;
        private readonly ISaveLoadService _saveLoadService;
        private readonly int _winReward;
        private readonly int _defeatReward;
        private readonly PlayerScore _score;
        private readonly StagesData _stagesData;
        private readonly AudioPlayerService _audioPlayer;
        private readonly PluginYGLeaderboard _leaderboard;

        private int _earnedGold;

        public BattleEndModel(Game game, Wallet wallet, ISaveLoadService saveLoadService, int winReward, int defeatReward,
            PlayerScore score, StagesData stagesData, AudioPlayerService audioPlayer)
        {
            _game = game;
            _wallet = wallet;
            _saveLoadService = saveLoadService;
            _winReward = winReward;
            _defeatReward = defeatReward;
            _score = score;
            _stagesData = stagesData;
            _audioPlayer = audioPlayer;

            _leaderboard = new PluginYGLeaderboard(score);
        }

        public event Action PlayerWinned;
        public event Action PlayerLoosed;
        public event Action<int> ScoreChanged;
        public event Action<int> GoldAmountChanged;
        public event Action NextStageUnlocked;

        public void End(bool isPlayerWin, int npcCannonDamageTaken)
        {
            if (isPlayerWin)
            {
                _earnedGold = _winReward;
                PlayerWinned?.Invoke();
                _audioPlayer.PlayWinSound();

                if (_stagesData.UnlockNextStage())
                {
                    NextStageUnlocked?.Invoke();
                    _stagesData.ChangeStageToNextOne();
                }
            }
            else
            {
                _earnedGold = _defeatReward;
                PlayerLoosed?.Invoke();
                _audioPlayer.PlayDefeatSound();
            }

            _wallet.Add(_earnedGold);
            _score.Value += npcCannonDamageTaken;
            _leaderboard.UpdateScore();
            GoldAmountChanged?.Invoke(_earnedGold);
            ScoreChanged?.Invoke(npcCannonDamageTaken);

            _saveLoadService.SaveProgress();

            MetricsService.CallStageEndedEvent(_stagesData.SelectedStageName, isPlayerWin);
        }

        public void LoadMainMenu()
        {
            AdsService.ShowInterstitialAds();
            _game.LoadMainMenu();
        }

        public void LoadNextStage()
        {
            AdsService.ShowInterstitialAds();
            _game.LoadGameScene();
        }
    }
}
