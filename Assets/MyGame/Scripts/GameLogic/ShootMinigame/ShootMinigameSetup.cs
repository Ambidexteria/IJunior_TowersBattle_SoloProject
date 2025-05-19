using Base.GameLogic.Cannon;
using Base.Infrastructure;
using Base.Services.TimeManagment;
using Base.UI.Game.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameSetup : MonoBehaviour
    {
        [SerializeField] private ShootMinigamePressRangeView _pressRangeView;
        [SerializeField] private ShootMinigameView _shootMinigameView;

        [SerializeField] private Animator _luancherModelAnimator;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private ParticleSystemController _particleSystemController;

        [Range(0f, 1f)]
        [SerializeField] private float _pressRangeWidthCoefficient = 0.1f;
        [SerializeField] private float _sliderSpeedRate;
        [SerializeField] private Image _launcButtonView;
        [SerializeField] private RectTransform _fullRangeRectTransform;

        private ShootMinigameModel _shootMinigameModel;
        private ShootMinigamePresenter _shootMinigmamePresenter;

        private ShootMinigameLauncherModel _launcherModel;
        private ShootMinigameLauncherPresenter _launcherPresenter;

        private ShootMinigamePressRangeModel _pressRangeModel;
        private ShootMinigamePressRangePresenter _pressRangePresenter;

        public ShootMinigameModel CreateShootMinigameModel(CannonEnergyBar energyBar, TimeController timeController, 
            ICoroutineRunner coroutineRunner, GameUIStateMachine uiStateMachine)
        {
            _launcherModel = new ShootMinigameLauncherModel(_luancherModelAnimator,
                _disabledSprite, _enabledSprite, _particleSystemController);
            _launcherPresenter = new ShootMinigameLauncherPresenter(_launcherModel, _launcButtonView);
            _launcherPresenter.Enable();

            _pressRangeModel = new ShootMinigamePressRangeModel(_pressRangeWidthCoefficient, _sliderSpeedRate, 
                _fullRangeRectTransform, timeController, coroutineRunner);
            _pressRangePresenter = new ShootMinigamePressRangePresenter(_pressRangeModel, _pressRangeView);
            _pressRangePresenter.Enable();

            _shootMinigameModel = new ShootMinigameModel(energyBar, _launcherModel,
                _pressRangeModel, timeController, uiStateMachine);
            _shootMinigmamePresenter = new ShootMinigamePresenter(_shootMinigameModel, _shootMinigameView);
            _shootMinigmamePresenter.Enable();

            return _shootMinigameModel;
        }
    }
}
