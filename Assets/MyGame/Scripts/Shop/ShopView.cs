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

        [SerializeField] private TextMeshProUGUI _currentGoldAmount;

        [SerializeField] private ButtonClickHandler _rewardAdsButton;

        public event Action CannonDamageUpgradeClicked;
        public event Action CannonHealthUpgradeClicked;
        public event Action SpawnTimeUpgradeClicked;

        public event Action RewardAdsClicked;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShopView), nameof(Awake),
                _cannonDamageUpgrade, _cannonDamageUpgradeLevel, _cannonDamageUpgradePrice,
                _cannonHealthUpgrade, _cannonHealthUpgradeLevel, _cannonHealthUpgradePrice,
                _spawnTimeUpgrade, _spawnTimeUpgradeLevel, _spawnTimeUpgradePrice,
                _currentGoldAmount, _rewardAdsButton);
        }

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

        public void DisplaySpawnTimeUpgradeLevel(string text)
        {
            _spawnTimeUpgradeLevel.text = text;
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
