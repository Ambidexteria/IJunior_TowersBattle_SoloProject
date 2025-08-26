using System;
using Base.Shop;

namespace Base.Data.Game
{
    [Serializable]
    public class GameData
    {
        public PlayerData PlayerData;
        public UpgradePrices UpgradePrices;
        public StagesData StagesData;
        public GameSettings GameSettings;

        public GameData()
        {
            PlayerData = new PlayerData();
            UpgradePrices = new UpgradePrices();
            StagesData = new StagesData();
            GameSettings = new GameSettings();
        }
    }
}
