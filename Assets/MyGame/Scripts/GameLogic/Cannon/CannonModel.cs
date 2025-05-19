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
        private Health _health;
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
            Barrel barrel, int damage, float maxHealth, 
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
            _health = new Health(maxHealth);

            Recolor();
        }

        public float MaxHealth => _health.MaxValue;
        public float CurrentHealth => _health.Current;
        public int Damage => _damage;
        public Transform Transform => _transfrom;

        public event Action Destroyed;
        public event Action<float> HealthChanged;

        public void Enable()
        {
            _health.Dying += OnDying;
            _triggerObserver.Entered += OnTriggerCollided;
        }

        public void Disable()
        {
            _health.Dying -= OnDying;
            _triggerObserver.Entered -= OnTriggerCollided;
        }

        public void SetEnemy(CannonModel enemy)
        {
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

        public void TakeDamage(int amount)
        {
            Debug.Log("Cannon takes damage");
            _health.Decrease(amount);
            _takeDamageEffect.Play();
            HealthChanged?.Invoke(_health.Current);
        }
        private void Recolor()
        {
            if (_colorChangerMarks != null)
                _colorChanger.Recolor(_team, _colorChangerMarks);
        }

        private void OnTriggerCollided(Collider collider)
        {
            Debug.Log($"{nameof(CannonModel)} - Collision detected with {collider.transform.root.name}");

            if(collider.TryGetComponent(out CannonProjectile projectile))
            {
                Debug.Log($"{nameof(CannonModel)} - Projectile detected");

                if (projectile.TeamType != _team.Type)
                {
                    Debug.Log($"{nameof(CannonModel)} - Different team detected");

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
