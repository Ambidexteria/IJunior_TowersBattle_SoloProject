using System;
using Base.Health;
using Base.Services.PluginYG;

namespace Base.UI.RewardForAds
{
    public class RestoreHealthForRewardAdsModel
    {
        private const float RestoreHealthPart = 0.3f;

        private readonly HealthModel _healthModel;

        public RestoreHealthForRewardAdsModel(HealthModel healthModel)
        {
            _healthModel = healthModel;
        }

        public event Action<bool> RewardGained;

        public void ShowRewardAds()
        {
            AdsService.ShowRewardedAdsForHealth(GetReward, CalculateRestoredHealth());
        }

        public void RejectReward()
        {
            RewardGained?.Invoke(false);
        }

        private void GetReward()
        {
            _healthModel.Increase(CalculateRestoredHealth());
            RewardGained?.Invoke(true);
        }

        private int CalculateRestoredHealth()
        {
            return (int)(_healthModel.MaxValue * RestoreHealthPart);
        }
    }
}