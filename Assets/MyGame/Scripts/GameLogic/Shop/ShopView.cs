using System;
using TMPro;
using UnityEngine;

namespace Base.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _cannonDamageUpgrade;
        [SerializeField] private TextMeshProUGUI _cannonDamageUpgradeLevel;
        [SerializeField] private TextMeshProUGUI _cannonDamageUpgradePrice;

        [SerializeField] private ButtonClickHandler _cannonHealthUpgrade;
        [SerializeField] private TextMeshProUGUI _cannonHealthUpgradeLevel;
        [SerializeField] private TextMeshProUGUI _cannonHealthUpgradePrice;

        [SerializeField] private ButtonClickHandler _spawnTimeUpgrade;
        [SerializeField] private TextMeshProUGUI _spawnTimeUpgradeLevel;
        [SerializeField] private TextMeshProUGUI _spawnTimeUpgradePrice;

        [SerializeField] private ButtonClickHandler _soldierDamageUpgrade;
        [SerializeField] private TextMeshProUGUI _soldierDamageUpgradeLevel;
        [SerializeField] private TextMeshProUGUI _soldierDamageUpgradePrice;

        [SerializeField] private ButtonClickHandler _soldierHealthUpgrade;
        [SerializeField] private TextMeshProUGUI _soldierHealthUpgradeLevel;
        [SerializeField] private TextMeshProUGUI _soldierHealthUpgradePrice;

        [SerializeField] private TextMeshProUGUI _currentGoldAmount;

        [SerializeField] private TextMeshProUGUI _rewardCoinsAmount;
        [SerializeField] private ButtonClickHandler _rewardAdsButton;

        public event Action CannonDamageUpgradeClicked;
        public event Action CannonHealthUpgradeClicked;
        public event Action SpawnTimeUpgradeClicked;
        public event Action SoldierDamageUpgradeClicked;
        public event Action SoldierHealthUpgradeClicked;

        public event Action RewardAdsClicked;

        private void OnEnable()
        {
            _cannonDamageUpgrade.Clicked += OnCannonDamageUpgradeClicked;
            _cannonHealthUpgrade.Clicked += OnCannonHealthUpgradeClicked;
            _spawnTimeUpgrade.Clicked += OnSpawnTimeUpgradeClicked;
            _soldierDamageUpgrade.Clicked += OnSoldierDamageUpgradeClicked;
            _soldierHealthUpgrade.Clicked += OnSoldierHealthUpgradeClicked;

            _rewardAdsButton.Clicked += OnRewardAdsButtonClicked;
        }

        private void OnDisable()
        {
            _cannonDamageUpgrade.Clicked -= OnCannonDamageUpgradeClicked;
            _cannonHealthUpgrade.Clicked -= OnCannonHealthUpgradeClicked;
            _spawnTimeUpgrade.Clicked -= OnSpawnTimeUpgradeClicked;
            _soldierDamageUpgrade.Clicked -= OnSoldierDamageUpgradeClicked;
            _soldierHealthUpgrade.Clicked -= OnSoldierHealthUpgradeClicked;

            _rewardAdsButton.Clicked -= OnRewardAdsButtonClicked;
        }

        public void DisplayCurrentGold(int amount)
        {
            _currentGoldAmount.text = amount.ToString();
        }

        public void DisplayRewardCoinsAmount(int amount)
        {
            _rewardCoinsAmount.text = amount.ToString();
        }

        public void DisplayCannonDamageUpgradeLevel(string text)
        {
            _cannonDamageUpgradeLevel.text = text;
        }

        public void DisplayCannonHealthUpgradeLevel(string text)
        {
            _cannonHealthUpgradeLevel.text = text;
        }

        public void DisplaySpawnTimeUpgradeLevel(string text)
        {
            _spawnTimeUpgradeLevel.text = text;
        }

        public void DisplaySoldierDamageUpgradeLevel(string text)
        {
            _soldierDamageUpgradeLevel.text = text;
        }

        public void DisplaySoldierHealthUpgradeLevel(string text)
        {
            _soldierHealthUpgradeLevel.text = text;
        }

        public void SetCannonHealthUpgradePrice(int price)
        {
            _cannonHealthUpgradePrice.text = price.ToString();
        }

        public void SetCannonDamageUpgradePrice(int price)
        {
            _cannonDamageUpgradePrice.text = price.ToString();
        }

        public void SetSpawnTimeUpgradePrice(int price)
        {
            _spawnTimeUpgradePrice.text = price.ToString();
        }

        public void SetSoldierDamageUpgradePrice(int price)
        {
            _soldierDamageUpgradePrice.text = price.ToString();
        }

        public void SetSoldierHealthUpgradePrice(int price)
        {
            _soldierHealthUpgradePrice.text = price.ToString();
        }

        private void OnCannonDamageUpgradeClicked()
        {
            CannonDamageUpgradeClicked?.Invoke();
        }

        private void OnCannonHealthUpgradeClicked()
        {
            CannonHealthUpgradeClicked?.Invoke();
        }

        private void OnSpawnTimeUpgradeClicked()
        {
            SpawnTimeUpgradeClicked?.Invoke();
        }

        private void OnSoldierDamageUpgradeClicked()
        {
            SoldierDamageUpgradeClicked?.Invoke();
        }

        private void OnSoldierHealthUpgradeClicked()
        {
            SoldierHealthUpgradeClicked?.Invoke();
        }

        private void OnRewardAdsButtonClicked()
        {
            RewardAdsClicked?.Invoke();
        }
    }
}
