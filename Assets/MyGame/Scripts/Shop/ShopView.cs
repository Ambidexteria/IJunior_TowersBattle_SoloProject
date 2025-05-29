using System;
using TMPro;
using UnityEngine;

namespace Base.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _cannonDamageUpgrade;
        [SerializeField] private TextMeshProUGUI _cannonDamageUpgradeLevel;

        [SerializeField] private ButtonClickHandler _cannonHealthUpgrade;
        [SerializeField] private TextMeshProUGUI _cannonHealthUpgradeLevel;
        [SerializeField] private TextMeshProUGUI _cannonHealthUpgradePrice;

        [SerializeField] private ButtonClickHandler _spawnTimeUpgrade;
        [SerializeField] private TextMeshProUGUI _spawnTimeUpgradeLevel;

        [SerializeField] private TextMeshProUGUI _currentGoldAmount;

        [SerializeField] private ButtonClickHandler _rewardAdsButton;

        public event Action CannonDamageUpgradeClicked;
        public event Action CannonHealthUpgradeClicked;
        public event Action SpawnTimeUpgradeClicked;

        public event Action RewardAdsClicked;

        private void OnEnable()
        {
            _cannonDamageUpgrade.Clicked += OnCannonDamageUpgradeClicked;
            _cannonHealthUpgrade.Clicked += OnCannonHealthUpgradeClicked;
            _spawnTimeUpgrade.Clicked += OnSpawnTimeUpgradeClicked;

            _rewardAdsButton.Clicked += OnRewardAdsButtonClicked;
        }

        private void OnDisable()
        {
            _cannonDamageUpgrade.Clicked -= OnCannonDamageUpgradeClicked;
            _cannonHealthUpgrade.Clicked -= OnCannonHealthUpgradeClicked;
            _spawnTimeUpgrade.Clicked -= OnSpawnTimeUpgradeClicked;

            _rewardAdsButton.Clicked -= OnRewardAdsButtonClicked;
        }

        public void DisplayCurrentGold(int amount)
        {
            _currentGoldAmount.text = amount.ToString();
        }

        public void DisplayCannonDamageUpgradeLevel(string text)
        {
            _cannonDamageUpgradeLevel.text = text;
        }

        public void DisplayCannonHealthUpgradeLevel(string text)
        {
            _cannonHealthUpgradeLevel.text = text;
        }

        public void SetHealthUpgradePrice(int price)
        {
            _cannonHealthUpgradePrice.text = price.ToString();
        }

        public void DisplaySpawnTimeUpgradeLevel(string text)
        {
            _spawnTimeUpgradeLevel.text = text;
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

        private void OnRewardAdsButtonClicked()
        {
            RewardAdsClicked?.Invoke();
        }
    }
}
