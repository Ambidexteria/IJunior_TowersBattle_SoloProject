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
        public Upgrades Upgrades = new();
        public UpgradePrices UpgradePrices = new();
        public StagesData StagesData  =new()    ;
        public GameSettings GameSettings = new();
        public SoldierData SoldierData = new();
        public CannonData CannonData = new();
        public AudioVolumeSettings AudioVolumeSettings = new();
        public int CurrentGold = 300;
        public PlayerScore Score = new();
    }
}
