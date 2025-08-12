using Base.Health;
using Base.Logic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonModel : IDamageable
    {
        private const string BarrelDrawback = nameof(BarrelDrawback);

        private readonly Animator _animator;
        private readonly HealthModel _health;
        private readonly ParticleSystemController _shootEffect;
        private readonly ParticleSystemController _takeDamageEffect;
        private readonly Barrel _barrel;
        private readonly int _damage;
        private readonly float _fireDelay;
        private readonly TeamColorChanger _colorChanger;
        private readonly Transform _transfrom;
        private readonly TriggerObserver _triggerObserver;
        private readonly Team _team;
        private readonly CannonProjectileSpawner _projectileSpawner;
        private readonly List<ColorChangerMark> _colorChangerMarks;

        private CannonModel _enemyCannon;
        private bool _enabled = false;

        public CannonModel(Transform transfrom, TriggerObserver triggerObserver, Team team, Animator animator, 
            ParticleSystemController shootEffect, ParticleSystemController takeDamageEffect,
            Barrel barrel, int damage, HealthModel health,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            List<ColorChangerMark> marksForRecoloring = null)
        {
            _transfrom = transfrom;
            _triggerObserver = triggerObserver;
            _team = team;
            _animator = animator;
            _shootEffect = shootEffect;
            _takeDamageEffect = takeDamageEffect;
            _barrel = barrel;
            _damage = damage;
            _colorChangerMarks = marksForRecoloring;
            _projectileSpawner = projectileSpawner;
            _colorChanger = colorChanger;
            _health = health;

            Recolor();
        }

        public int DamageTaken => (int)(_health.MaxValue - _health.Current);
        public Transform Transform => _transfrom;

        public event Action Destroyed;
        public event Action<float> HealthChanged;

        public void Enable()
        {
            if (_enabled)
                return;

            if(_enemyCannon == null)
                throw new NullReferenceException(nameof(_enemyCannon));

            _enabled = true;

            _health.Dying += OnDying;
            _triggerObserver.Entered += OnTriggerCollided;
            _health.Changed += HealthChanged;
        }

        public void Disable()
        {
            if(_enabled == false)
                return;

            _enabled = false;

            _health.Dying -= OnDying;
            _triggerObserver.Entered -= OnTriggerCollided;
            _health.Changed -= HealthChanged;
        }

        public void SetEnemy(CannonModel enemy)
        {
            _enemyCannon = enemy;
        }

        public bool IsDead()
        {
            return _health.IsDead;
        }

        public void Shoot()
        {
            CannonProjectile cannonProjectile = _projectileSpawner.Spawn();

            cannonProjectile.transform.position = _barrel.StartPoint;
            cannonProjectile.Init(_team, _barrel.StartPoint, _enemyCannon.Transform.position, _damage);
            cannonProjectile.gameObject.SetActive(true);

            _shootEffect.Play();
            _animator.Play(BarrelDrawback);
        }

        public void TakeDamage(float amount)
        {
            _health.SmoothDecrease(amount);
            _takeDamageEffect.Play();
        }

        private void Recolor()
        {
            if (_colorChangerMarks != null)
                _colorChanger.Recolor(_team, _colorChangerMarks);
        }

        private void OnTriggerCollided(Collider collider)
        {
            if (collider.TryGetComponent(out CannonProjectile projectile))
            {
                if (projectile.TeamType != _team.Type)
                {
                    TakeDamage(projectile.Damage);
                    projectile.Despawn();
                }
            }
        }

        private void OnDying()
        {
            Destroyed?.Invoke();
        }
    }
}
