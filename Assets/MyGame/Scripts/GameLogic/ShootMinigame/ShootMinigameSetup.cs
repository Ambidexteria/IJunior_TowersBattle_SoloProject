using Base.GameLogic.Cannon;
using Base.Infrastructure;
using Base.Services.TimeManagment;
using UnityEngine;
using UnityEngine.UI;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameSetup : MonoBehaviour
    {
        [SerializeField] private ShootMinigamePressRangeView _pressRangeView;
        [SerializeField] private ShootMinigameView _shootMinigameView;

        [SerializeField] private Animator _launcherModelAnimator;
        [SerializeField] private ParticleSystemController _launchMinigameButtonEffect;

        [Range(0f, 1f)]
        [SerializeField] private float _pressRangeWidthCoefficient = 0.1f;
        [SerializeField] private float _sliderSpeedRate;
        [SerializeField] private Image _launchButtonView;
        [SerializeField] private RectTransform _fullRangeRectTransform;

        private ShootMinigameModel _shootMinigameModel;
        private ShootMinigamePresenter _shootMinigmamePresenter;

        private ShootMinigameLauncherModel _launcherModel;

        private ShootMinigamePressRangeModel _pressRangeModel;
        private ShootMinigamePressRangePresenter _pressRangePresenter;

        public float FullRangeMinValue => _fullRangeRectTransform.anchoredPosition.x;
        public float FullRangeMaxValue => _fullRangeRectTransform.rect.width;


        public ShootMinigameModel CreateShootMinigameModel(CannonEnergyBarModel energyBar, TimeController timeController, 
            ICoroutineRunner coroutineRunner)
        {
            _launcherModel = new ShootMinigameLauncherModel(_launcherModelAnimator, _launchMinigameButtonEffect);
            _pressRangeModel = new ShootMinigamePressRangeModel(_pressRangeWidthCoefficient, _sliderSpeedRate, 
                _fullRangeRectTransform, timeController, coroutineRunner);

            _pressRangeView.SetMinMaxValues(_fullRangeRectTransform.anchoredPosition.x, _fullRangeRectTransform.rect.width);
            _pressRangePresenter = new ShootMinigamePressRangePresenter(_pressRangeModel, _pressRangeView);
            _pressRangePresenter.Enable();

            _shootMinigameModel = new ShootMinigameModel(energyBar, _launcherModel, _pressRangeModel);
            _shootMinigmamePresenter = new ShootMinigamePresenter(_shootMinigameModel, _shootMinigameView);
            _shootMinigmamePresenter.Enable();

            return _shootMinigameModel;
        }
    }
}
