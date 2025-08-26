using Base.GameLogic.UpgradeSystem;
using Base.PLayer;
using Base.Services.PersistentProgress;
using Base.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Base.Shop
{
    public class ShopSetup : MonoBehaviour
    {
        [SerializeField] private ShopView _view;

        private Wallet _wallet;
        private RegularUpgradeSystem _upgradeSystem;
        private ISaveLoadService _saveLoadService;
        private IPersisentDataService _dataService;

        private ShopModel _model;
        private ShopPresenter _presenter;

        [Inject]
        private void Init(
            Wallet wallet, 
            RegularUpgradeSystem upgradeSystem, 
            ISaveLoadService saveLoadService,
            IPersisentDataService prices)
        {
            _wallet = wallet;
            _upgradeSystem = upgradeSystem;
            _saveLoadService = saveLoadService;
            _dataService = prices;
        }

        private void OnEnable()
        {
            CreateModel();
        }

        private void OnDisable()
        {
            Disable();
        }

        private void CreateModel()
        {
            _model = new ShopModel(_wallet, _upgradeSystem, _saveLoadService, _dataService.GameData.UpgradePrices);

            _presenter = new ShopPresenter(_view, _model);
            _presenter.Enable();

            _model.Enable();
        }

        private void Disable()
        {
            _presenter.Disable();
            _model = null;
        }
    }
}
