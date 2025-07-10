using System;
using UnityEngine;
using YG;

namespace Base.Services.PluginYG
{
    public class AdsService : MonoBehaviour
    {
        private const string Money = nameof(Money);
        private const string Health = nameof(Health);

        public static void ShowInterstitialAds()
        {
            YG2.InterstitialAdvShow();
            MetricsService.CallInterAdsEvent();
        }

        public static void ShowRewardedAdsForMoney(Action adsShowed, int amount)
        {
            YG2.RewardedAdvShow(Money, adsShowed);
            MetricsService.CallRewardedAdsEvent(Money, amount);

        }

        public static void ShowRewardedAdsForHealth(Action adsShowed, int amount)
        {
            YG2.RewardedAdvShow(Health, adsShowed);
            MetricsService.CallRewardedAdsEvent(Health, amount);
        }
    }
}
