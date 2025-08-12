namespace Base.Shop
{
    public class ShopPresenter
    {
        private readonly ShopView _view;
        private readonly ShopModel _model;

        public ShopPresenter(ShopView view, ShopModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.DisplayRewardCoinsAmount(_model.RewardCoinsAmount);

            _view.SetCannonHealthUpgradePrice(_model.CannonHealthUpgradePrice);
            _view.SetCannonDamageUpgradePrice(_model.CannonDamageUpgradePrice);
            _view.SetSpawnTimeUpgradePrice(_model.SpawnTimeUpgradePrice);
            _view.SetSoldierDamageUpgradePrice(_model.SoldierDamageUpgradePrice);
            _view.SetSoldierHealthUpgradePrice(_model.SoldierHealthUpgradePrice);

            _view.CannonHealthUpgradeClicked += OnCannonHealthUpgradeClicked;
            _view.CannonDamageUpgradeClicked += OnCannonDamageUpgradeClicked;
            _view.SpawnTimeUpgradeClicked += OnSpawnTimeUpgradeClicked;
            _view.SoldierDamageUpgradeClicked += OnSoldierDamageUpgradeClicked;
            _view.SoldierHealthUpgradeClicked += OnSoldierHealthUpgradeClicked;
            _view.RewardAdsClicked += OnRewardAdsClicked;

            _model.HealthUpgradeLevelChanged += OnCannonHealthUpgradeLevelIncreased;
            _model.CannonDamageUpgradeLevelChanged += OnCannonDamageUpgradeLevelIncreased;
            _model.SpawnTimehUpgradeLevelChanged += OnSpawnTimeUpgradeLevelIncreased;
            _model.SoldierDamageUpgradeLevelChanged += OnSoldierDamageLevelIncreased;
            _model.SoldierHealthUpgradeLevelChanged += OnSoldierHealthUpgradeLevelIncreased;

            _model.CurrentGoldChanged += OnCurrentGoldChanged;
        }

        public void Disable()
        {
            _view.CannonHealthUpgradeClicked -= OnCannonHealthUpgradeClicked;
            _view.CannonDamageUpgradeClicked -= OnCannonDamageUpgradeClicked;
            _view.SpawnTimeUpgradeClicked -= OnSpawnTimeUpgradeClicked;
            _view.SoldierDamageUpgradeClicked -= OnSoldierDamageUpgradeClicked;
            _view.SoldierHealthUpgradeClicked -= OnSoldierHealthUpgradeClicked;
            _view.RewardAdsClicked -= OnRewardAdsClicked;

            _model.HealthUpgradeLevelChanged -= OnCannonHealthUpgradeLevelIncreased;
            _model.CannonDamageUpgradeLevelChanged -= OnCannonDamageUpgradeLevelIncreased;
            _model.SpawnTimehUpgradeLevelChanged -= OnSpawnTimeUpgradeLevelIncreased;
            _model.SoldierDamageUpgradeLevelChanged -= OnSoldierDamageLevelIncreased;
            _model.SoldierHealthUpgradeLevelChanged -= OnSoldierHealthUpgradeLevelIncreased;

            _model.CurrentGoldChanged -= OnCurrentGoldChanged;
        }

        private void OnCurrentGoldChanged(int amount)
        {
            _view.DisplayCurrentGold(amount);
        }

        private void OnRewardAdsClicked()
        {
            _model.ShowRewardAds();
        }

        private void OnCannonHealthUpgradeLevelIncreased(string level)
        {
            _view.DisplayCannonHealthUpgradeLevel(level);
        }

        private void OnCannonDamageUpgradeLevelIncreased(string level)
        {
            _view.DisplayCannonDamageUpgradeLevel(level);
        }

        private void OnSpawnTimeUpgradeLevelIncreased(string level)
        {
            _view.DisplaySpawnTimeUpgradeLevel(level);
        }

        private void OnSoldierDamageLevelIncreased(string level)
        {
            _view.DisplaySoldierDamageUpgradeLevel(level);
        }

        private void OnSoldierHealthUpgradeLevelIncreased(string level)
        {
            _view.DisplaySoldierHealthUpgradeLevel(level);
        }

        private void OnCannonHealthUpgradeClicked()
        {
            _model.BuyCannonHealthUpgrade();
        }

        private void OnSpawnTimeUpgradeClicked()
        {
            _model.BuySpawnTimeUpgrade();
        }

        private void OnCannonDamageUpgradeClicked()
        {
            _model.BuyCannonDamageUpgrade();
        }

        private void OnSoldierDamageUpgradeClicked()
        {
            _model.BuySoldierDamageUpgrade();
        } 
        
        private void OnSoldierHealthUpgradeClicked()
        {
            _model.BuySoldierHealthUpgrade();
        }
    }
}
