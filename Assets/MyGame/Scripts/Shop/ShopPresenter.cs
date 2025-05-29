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
            _view.CannonHealthUpgradeClicked += OnCannonHealthUpgradeClicked;

            _model.HealthUpgradeLevelChanged += OnHealthUpgradeLevelIncreased;
            _model.CurrentGoldChanged += OnCurrentGoldChanged;
        }

        public void Disable()
        {
            _view.CannonHealthUpgradeClicked -= OnCannonHealthUpgradeClicked;

            _model.HealthUpgradeLevelChanged -= OnHealthUpgradeLevelIncreased;
            _model.CurrentGoldChanged -= OnCurrentGoldChanged;
        }

        private void OnCurrentGoldChanged(int amount)
        {
            _view.DisplayCurrentGold(amount);
        }

        private void OnHealthUpgradeLevelIncreased(string level)
        {
            _view.DisplayCannonHealthUpgradeLevel(level);
        }

        private void OnCannonHealthUpgradeClicked()
        {
            _model.BuyCannonHealthUpgrade();
        }
    }
}
