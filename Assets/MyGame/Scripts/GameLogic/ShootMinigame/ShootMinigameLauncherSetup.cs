using UnityEngine;
using UnityEngine.UI;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameLauncherSetup : MonoBehaviour
    {
        [SerializeField] private Image _view;
        [SerializeField] private Animator _animator;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private ParticleSystemController _particleSystemController;

        private ShootMinigameLauncherModel _model;
        private ShootMinigameLauncherPresenter _presenter;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShootMinigameLauncherSetup), nameof(Awake), _view, _animator, _disabledSprite, 
                _enabledSprite, _particleSystemController);

            _model = new ShootMinigameLauncherModel(_animator, _disabledSprite, _enabledSprite, _particleSystemController);

            _presenter = new ShootMinigameLauncherPresenter(_model, _view);
        }
    }
}
