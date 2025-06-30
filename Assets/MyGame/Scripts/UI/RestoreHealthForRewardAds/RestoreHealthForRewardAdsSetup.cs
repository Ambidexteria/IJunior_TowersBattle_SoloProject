using Base.Health;
using UnityEngine;

namespace Base.UI.RewardForAds
{
    public class RestoreHealthForRewardAdsSetup : MonoBehaviour
    {
        [SerializeField] private RestoreHealthForRewardAdsView _view;

        private RestoreHealthForRewardAdsModel _model;
        private RestoreHealthForRewardAdsPresenter _presenter;

        public RestoreHealthForRewardAdsModel Create(HealthModel healthModel)
        {
            _model = new RestoreHealthForRewardAdsModel(healthModel);
            _presenter = new RestoreHealthForRewardAdsPresenter(_model, _view);

            _presenter.Enable();

            return _model;
        }
    }
}