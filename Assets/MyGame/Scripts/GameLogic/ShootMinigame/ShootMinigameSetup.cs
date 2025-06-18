using Base.GameLogic.Cannon;
using Base.Infrastructure;
using Base.Services.TimeManagment;
using Base.UI.StateMachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject.Asteroids;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameSetup : MonoBehaviour
    {
        [SerializeField] private ShootMinigamePressRangeView _pressRangeView;
        [SerializeField] private ShootMinigameView _shootMinigameView;

        [SerializeField] private Animator _launcherModelAnimator;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private ParticleSystemController _particleSystemController;

        [Range(0f, 1f)]
        [SerializeField] private float _pressRangeWidthCoefficient = 0.1f;
        [SerializeField] private float _sliderSpeedRate;
        [SerializeField] private Image _launchButtonView;
        [SerializeField] private RectTransform _fullRangeRectTransform;

        private ShootMinigameModel _shootMinigameModel;
        private ShootMinigamePresenter _shootMinigmamePresenter;

        private ShootMinigameLauncherModel _launcherModel;
        private ShootMinigameLauncherPresenter _launcherPresenter;

        private ShootMinigamePressRangeModel _pressRangeModel;
        private ShootMinigamePressRangePresenter _pressRangePresenter;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShootMinigameSetup), nameof(Awake), _pressRangeView, _shootMinigameView, 
                _launcherModelAnimator, _disabledSprite, _enabledSprite, _particleSystemController, _launchButtonView, 
                _fullRangeRectTransform);        
        }

        public ShootMinigameModel CreateShootMinigameModel(CannonEnergyBarModel energyBar, TimeController timeController, 
            ICoroutineRunner coroutineRunner)
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShootMinigameSetup), nameof(CreateShootMinigameModel), energyBar, timeController, coroutineRunner);

            _launcherModel = new ShootMinigameLauncherModel(_launcherModelAnimator,
                _disabledSprite, _enabledSprite, _particleSystemController);
            _launcherPresenter = new ShootMinigameLauncherPresenter(_launcherModel, _launchButtonView);
            _launcherPresenter.Enable();

            _pressRangeModel = new ShootMinigamePressRangeModel(_pressRangeWidthCoefficient, _sliderSpeedRate, 
                _fullRangeRectTransform, timeController, coroutineRunner);
            _pressRangePresenter = new ShootMinigamePressRangePresenter(_pressRangeModel, _pressRangeView);
            _pressRangePresenter.Enable();

            _shootMinigameModel = new ShootMinigameModel(energyBar, _launcherModel, _pressRangeModel);
            _shootMinigmamePresenter = new ShootMinigamePresenter(_shootMinigameModel, _shootMinigameView);
            _shootMinigmamePresenter.Enable();

            return _shootMinigameModel;
        }
    }
}
