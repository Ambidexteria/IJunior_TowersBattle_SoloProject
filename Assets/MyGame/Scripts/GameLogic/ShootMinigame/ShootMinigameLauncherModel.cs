using System;
using UnityEngine;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameLauncherModel
    {
        private const string Scale = nameof(Scale);

        private readonly Animator _animator;
        private readonly ParticleSystemController _particleSystemController;

        public ShootMinigameLauncherModel(Animator animator, ParticleSystemController particleSystemController)
        {
            _animator = animator;
            _particleSystemController = particleSystemController;
        }

        public void Enable()
        {
            _particleSystemController.Play();
            _animator.Play(Scale);
        }

        public void Disable()
        {
            _particleSystemController.Stop();
        }
    }
}
