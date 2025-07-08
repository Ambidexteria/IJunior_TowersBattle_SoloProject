using Base.Health;
using Base.Services.PluginYG;
using System;
using YG;

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
            AdsService.ShowRewardedAdsForHealth(GetReward);
        }

        public void RejectReward()
        {
            RewardGained?.Invoke(false);
        }

        private void GetReward()
        {
            _healthModel.Increase(_healthModel.MaxValue * RestoreHealthPart);
            RewardGained?.Invoke(true);
        }
    }
}