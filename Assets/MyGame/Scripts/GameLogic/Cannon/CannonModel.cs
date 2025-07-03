using Base.Health;
using Base.Infrastructure;
using Base.Logic;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Base.GameLogic.Cannon
{
    public class CannonModel : IDamageable
    {
        private const string BarrelDrawback = nameof(BarrelDrawback);

        private Animator _animator;
        private CannonModel _enemyCannon;
        private HealthModel _health;
        private ParticleSystemController _shootEffect;
        private ParticleSystemController _takeDamageEffect;
        private Barrel _barrel;
        private int _damage;
        private float _fireDelay;

        private TeamColorChanger _colorChanger;
        private Transform _transfrom;
        private TriggerObserver _triggerObserver;
        private Team _team;
        private CannonProjectileSpawner _projectileSpawner;
        private List<ColorChangerMark> _colorChangerMarks;

        public CannonModel(Transform transfrom, TriggerObserver triggerObserver, Team team, Animator animator, ParticleSystemController shootEffect, ParticleSystemController takeDamageEffect,
            Barrel barrel, int damage, HealthModel health,
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger,
            List<ColorChangerMark> marksForRecoloring = null)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonModel), ExceptionsTest.ConstructorName, transfrom, triggerObserver, team, 
                animator, shootEffect, takeDamageEffect, barrel, health,projectileSpawner, colorChanger, marksForRecoloring);
            ExceptionsTest.EmptyListTest(nameof(CannonModel), ExceptionsTest.ConstructorName, marksForRecoloring);

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
        public int Damage => _damage;
        public Transform Transform => _transfrom;

        public event Action Destroyed;
        public event Action<float> HealthChanged;

        public void Enable()
        {
            _health.Dying += OnDying;
            _triggerObserver.Entered += OnTriggerCollided;
            _health.Changed += HealthChanged;
        }

        public void Disable()
        {
            _health.Dying -= OnDying;
            _triggerObserver.Entered -= OnTriggerCollided;
        }

        public void SetEnemy(CannonModel enemy)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonModel), nameof(SetEnemy), enemy);

            _enemyCannon = enemy;
        }

        public TeamType GetTeamType() => _team.Type;

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
            //HealthChanged?.Invoke(_health.Current);
        }
        private void Recolor()
        {
            if (_colorChangerMarks != null)
                _colorChanger.Recolor(_team, _colorChangerMarks);
        }

        private void OnTriggerCollided(Collider collider)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonModel), nameof(OnTriggerCollided), collider);

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
