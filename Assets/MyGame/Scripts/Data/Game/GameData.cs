using Base.GameLogic.UpgradeSystem;
using Base.Shop;
using Newtonsoft.Json;
using System;

namespace Base.Data.Game
{
    [Serializable]
    public class GameData
    {
        [JsonRequired]
        public Upgrades Upgrades;
        public UpgradePrices UpgradePrices;
        public StagesData StagesData    ;
        public GameSettings GameSettings;
        public SoldierData SoldierData;
        public CannonData CannonData;
        public AudioVolumeSettings AudioVolumeSettings;
        public int CurrentGold;

        public GameData()
        {
            Upgrades = new();
            UpgradePrices = new();
            StagesData = new();
            GameSettings = new(StagesData.GetAllStages()[0]);
            SoldierData = new();
            CannonData = new();
            AudioVolumeSettings = new();
            CurrentGold = 10000;
        }
    }
}
