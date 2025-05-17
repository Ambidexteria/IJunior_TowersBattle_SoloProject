using UnityEngine;
using UnityEngine.UI;

namespace Base.GameLogic.ShootMinigame
{
    public class LaunchShootMinigameSetup : MonoBehaviour
    {
        [SerializeField] private Image _view;
        [SerializeField] private Animator _animator;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private ParticleSystemController _particleSystemController;

        private ShootMinigameLauncherModel _model;
        private LaunchShootMinigamePresenter _presenter;

        private void Awake()
        {
            _model = new ShootMinigameLauncherModel(_animator, _disabledSprite, _enabledSprite, _particleSystemController);

            _presenter = new LaunchShootMinigamePresenter(_model, _view);
        }
    }
}
