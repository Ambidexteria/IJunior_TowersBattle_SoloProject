using System;
using UnityEngine;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameLauncherModel
    {
        private const string Scale = nameof(Scale);
        private readonly Animator _animator;
        private readonly Sprite _disabledSprite;
        private readonly Sprite _enabledSprite;
        private readonly ParticleSystemController _particleSystemController;

        public ShootMinigameLauncherModel(Animator animator, Sprite disabledSprite, Sprite enabledSprite,
            ParticleSystemController particleSystemController)
        {
            _animator = animator;
            _disabledSprite = disabledSprite;
            _enabledSprite = enabledSprite;
            _particleSystemController = particleSystemController;
        }

        public event Action<Sprite> StatusChanged;

        public void Enable()
        {
            StatusChanged?.Invoke(_enabledSprite);
            _particleSystemController.Play();
            _animator.Play(Scale);
            Debug.Log("Play Scale anim");
        }

        public void Disable()
        {
            StatusChanged?.Invoke(_disabledSprite);
            _particleSystemController.Stop();
        }
    }
}
