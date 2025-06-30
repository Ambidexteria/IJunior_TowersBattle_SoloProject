using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.UI.RewardForAds
{
    public class RestoreHealthForRewardAdsView : MonoBehaviour
    {
        [SerializeField] private ButtonClickHandler _gainRewardButton;
        [SerializeField] private ButtonClickHandler _rejectRewardButton;

        public event Action RewardButtonClicked;
        public event Action RejectButtonClicked;

        private void OnEnable()
        {
            _gainRewardButton.Clicked += OnRewardButtonClicked;
            _rejectRewardButton.Clicked += OnRejectButtonClicked;
        }

        private void OnDisable()
        {
            _gainRewardButton.Clicked -= OnRewardButtonClicked;
            _rejectRewardButton.Clicked -= OnRejectButtonClicked;
        }

        private void OnRewardButtonClicked()
        {
            RewardButtonClicked?.Invoke();
        }

        private void OnRejectButtonClicked()
        {
            RejectButtonClicked?.Invoke();
        }
    }
}
