using Base.GameLogic.UpgradeSystem;
using Base.PLayer;
using Base.Services.SaveLoad;
using UnityEngine;

namespace Base.Shop
{
    public class ShopSetup : MonoBehaviour
    {
        [SerializeField] private ShopView _view;

        private ShopModel _model;
        private ShopPresenter _presenter;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShopSetup), nameof(Awake), _view);
        }

        public ShopModel Create(Wallet wallet, RegularUpgradeSystem upgradeSystem, ISaveLoadService saveLoadService,
            UpgradePrices prices)
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShopSetup), nameof(Create), wallet, upgradeSystem, saveLoadService, prices);

            _model = new ShopModel(wallet, upgradeSystem, saveLoadService, prices);

            _presenter = new ShopPresenter(_view, _model);
            _presenter.Enable();

            _model.Enable();

            return _model;
        }
    }
}
