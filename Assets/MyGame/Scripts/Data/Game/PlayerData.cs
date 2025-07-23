using Base.GameLogic.UpgradeSystem;
using System;

namespace Base.Data.Game
{
    [Serializable]
    public class PlayerData
    {
        public Upgrades Upgrades;
        public SoldierData SoldierData;
        public CannonData CannonData;
        public int CurrentGold;
        public PlayerScore Score;

        public PlayerData()
        {
            Upgrades = new();
            SoldierData = new();
            CannonData = new();
            CurrentGold = 200;
            Score = new PlayerScore();
        }
    }
}
