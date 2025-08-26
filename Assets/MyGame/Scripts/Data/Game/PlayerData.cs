using System;
using Base.GameLogic.UpgradeSystem;

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
            Upgrades = new Upgrades();
            SoldierData = new SoldierData();
            CannonData = new CannonData();
            CurrentGold = 200;
            Score = new PlayerScore();
        }
    }
}
