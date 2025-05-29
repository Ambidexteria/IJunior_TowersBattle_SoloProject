using Base.GameLogic;
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

        public ShopModel Create(Wallet wallet, RegularUpgradeSystem upgradeSystem, ISaveLoadService saveLoadService,
            UpgradePrices prices)
        {
            _model = new ShopModel(wallet, upgradeSystem, saveLoadService, prices);

            _presenter = new ShopPresenter(_view, _model);
            _presenter.Enable();

            _model.Enable();

            return _model;
        }
    }
}
