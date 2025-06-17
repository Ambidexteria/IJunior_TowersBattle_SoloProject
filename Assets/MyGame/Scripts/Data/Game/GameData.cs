using Base.GameLogic.UpgradeSystem;
using Base.Shop;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Base.Data.Game
{
    [Serializable]
    public class GameData
    {
        public Upgrades Upgrades;
        public UpgradePrices UpgradePrices;
        public StagesData StagesData    ;
        public GameSettings GameSettings;
        public SoldierData SoldierData;
        public CannonData CannonData;
        public AudioVolumeSettings AudioVolumeSettings;
        public int CurrentGold;
        public PlayerScore Score;

        public GameData()
        {
            Debug.Log("GameData - constructor");
            Upgrades = new();
            UpgradePrices = new();
            StagesData = new();
            GameSettings = new();
            SoldierData = new();
            CannonData = new();
            AudioVolumeSettings = new();
            CurrentGold = 300;
            Score = new();
        }
    }
}
