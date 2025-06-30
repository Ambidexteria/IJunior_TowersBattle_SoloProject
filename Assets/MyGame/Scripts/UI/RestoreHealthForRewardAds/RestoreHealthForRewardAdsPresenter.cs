namespace Base.UI.RewardForAds
{
    public class RestoreHealthForRewardAdsPresenter
    {
        private readonly RestoreHealthForRewardAdsModel _model;
        private readonly RestoreHealthForRewardAdsView _view;

        public RestoreHealthForRewardAdsPresenter(RestoreHealthForRewardAdsModel model, RestoreHealthForRewardAdsView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _view.RewardButtonClicked += OnRewardButtonCLicked;
            _view.RejectButtonClicked += OnRejectReward;
        }

        public void Disable()
        {
            _view.RewardButtonClicked -= OnRewardButtonCLicked;
            _view.RejectButtonClicked -= OnRejectReward;
        }

        private void OnRewardButtonCLicked()
        {
            _model.ShowRewardAds();
        } 
        
        private void OnRejectReward()
        {
            _model.RejectReward();
        }
    }
}