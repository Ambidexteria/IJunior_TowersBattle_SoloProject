using UnityEngine;
using YG;

namespace Base.Services.PluginYG
{
    public class MetricsService : MonoBehaviour
    {
        private const string Win = nameof(Win);
        private const string Defeat = nameof(Defeat);

        private const string FirstLaunch = nameof(FirstLaunch);
        private const string GameLaunched = nameof(GameLaunched);
        private const string StageEnded = nameof(StageEnded);
        private const string InterAds = nameof(InterAds);
        private const string RewardedAds = nameof(RewardedAds);
        private const string StageLoaded = nameof(StageLoaded);
        private const string UpgradeBought = nameof(UpgradeBought);

        public static void CallFirstLaunchEvent()
        {
            YG2.MetricaSend(FirstLaunch);
        }

        public static void CallGameLaunchedEvent()
        {
            YG2.MetricaSend(GameLaunched);
        }

        public static void CallStageEndedEvent(string stageName, bool isWin)
        {
            string battleResult;

            if (isWin)
                battleResult = Win;
            else
                battleResult = Defeat;

            YG2.MetricaSend(StageEnded, stageName, battleResult);
        }

        public static void CallStageLoadedEvent(string stageName)
        {
            YG2.MetricaSend(StageLoaded, stageName, stageName);
        }

        public static void CallInterAdsEvent()
        {
            YG2.MetricaSend(InterAds);
        }

        public static void CallRewardedAdsEvent(string reward, int amount)
        {
            YG2.MetricaSend(RewardedAds, reward, amount.ToString());
        }

        public static void CallUpgradeBoughtEvent(string upgradeName, string upgradeLevel)
        {
            YG2.MetricaSend(UpgradeBought, upgradeName, upgradeLevel);
        }
    }
}
