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
            _view.SetCannonHealthUpgradePrice(_model.CannonHealthUpgradePrice);
            _view.SetCannonDamageUpgradePrice(_model.CannonDamageUpgradePrice);
            _view.SetSpawnTimeUpgradePrice(_model.SpawnTimeUpgradePrice);

            _view.CannonHealthUpgradeClicked += OnCannonHealthUpgradeClicked;
            _view.CannonDamageUpgradeClicked += OnCannonDamageUpgradeClicked;
            _view.SpawnTimeUpgradeClicked += OnSpawnTimeUpgradeClicked;

            _model.HealthUpgradeLevelChanged += OnCannonHealthUpgradeLevelIncreased;
            _model.CannonDamageUpgradeLevelChanged += OnCannonDamageUpgradeLevelIncreased;
            _model.SpawnTimehUpgradeLevelChanged += OnSpawnTimeUpgradeLevelIncreased;
            _model.CurrentGoldChanged += OnCurrentGoldChanged;
        }

        public void Disable()
        {
            _view.CannonHealthUpgradeClicked -= OnCannonHealthUpgradeClicked;
            _view.CannonDamageUpgradeClicked -= OnCannonDamageUpgradeClicked;
            _view.SpawnTimeUpgradeClicked -= OnSpawnTimeUpgradeClicked;

            _model.HealthUpgradeLevelChanged -= OnCannonHealthUpgradeLevelIncreased;
            _model.CannonDamageUpgradeLevelChanged -= OnCannonDamageUpgradeLevelIncreased;
            _model.SpawnTimehUpgradeLevelChanged -= OnSpawnTimeUpgradeLevelIncreased;
            _model.CurrentGoldChanged -= OnCurrentGoldChanged;
        }

        private void OnCurrentGoldChanged(int amount)
        {
            _view.DisplayCurrentGold(amount);
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
    }
}
