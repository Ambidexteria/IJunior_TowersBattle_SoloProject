using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Base.Services.PluginYG
{
    public class MetricsService : MonoBehaviour
    {
        private const string GameLaunched = nameof(GameLaunched);
        private const string LevelWin = nameof(LevelWin);
        private const string LevelDefeat = nameof(LevelDefeat);
        private const string InterAds = nameof(InterAds);
        private const string RewardedAds = nameof(RewardedAds);

        public static void CallGameLaunchedEvent()
        {
            YG2.MetricaSend(GameLaunched);
        }

        public static void CallLevelWinEvent()
        {
            YG2.MetricaSend(LevelWin);
        }

        public static void CallLevelDefeatEvent()
        {
            YG2.MetricaSend(LevelDefeat);
        }

        public static void CallInterAdsEvent()
        {
            YG2.MetricaSend(InterAds);
        }

        public static void CallRewardedAdsEvent(string reward)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                { RewardedAds, reward }
            };

            YG2.MetricaSend(RewardedAds, dict);
        }
    }
}
