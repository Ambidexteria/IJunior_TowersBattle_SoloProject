using Base.Shop;
using System;

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
            PlayerData = new();
            UpgradePrices = new();
            StagesData = new();
            GameSettings = new();
        }
    }
}
