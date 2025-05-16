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

        [SerializeField] private Animator _animator;
        [SerializeField] private CannonModel _enemyCannon;
        [SerializeField] private Health _health;
        [SerializeField] private ParticleSystemController _shootEffect;
        [SerializeField] private ParticleSystemController _takeDamageEffect;
        [SerializeField] private Barrel _barrel;
        [SerializeField] private int _damage;
        [SerializeField] private float _fireDelay;

        private TeamColorChanger _colorChanger;
        private Transform _transfrom;
        private TriggerObserver _triggerObserver;
        private Team _team;
        private CannonProjectileSpawner _projectileSpawner;
        private List<ColorChangerMark> _colorChangerMarks;

        public CannonModel(Transform transfrom, TriggerObserver triggerObserver, Team team, Animator animator, ParticleSystemController shootEffect, ParticleSystemController takeDamageEffect,
            Barrel barrel, int damage, float firDelay, 
            CannonProjectileSpawner projectileSpawner, TeamColorChanger colorChanger, 
            List<ColorChangerMark> marksForRecoloring = null)
        {
            _transfrom = transfrom;
            _triggerObserver = triggerObserver;
            _triggerObserver.Entered += OnTriggerCollided;

            _team = team;
            _animator = animator;
            _shootEffect = shootEffect;
            _takeDamageEffect = takeDamageEffect;
            _barrel = barrel;
            _damage = damage;
            _fireDelay = firDelay;
            _colorChangerMarks = marksForRecoloring;
            _projectileSpawner = projectileSpawner;
            _colorChanger = colorChanger;
            _health = new Health(50);

            Awake();
            OnEnable();
        }

        public float MaxHealth => _health.MaxValue;
        public float CurrentHealth => _health.Current;
        public int Damage => _damage;
        public Transform Transform => _transfrom;

        public event Action Destroyed;
        public event Action<float> HealthChanged;

        private void Awake()
        {
            if (_colorChangerMarks != null)
                _colorChanger.Recolor(_team, _colorChangerMarks);
        }

        private void OnEnable()
        {
            _health.Dying += OnDying;
        }

        private void OnDisable()
        {
            _health.Dying -= OnDying;
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

        private void OnTriggerCollided(Collider collider)
        {
            if(collider.TryGetComponent(out CannonProjectile projectile))
            {
                if(projectile.TeamType != _team.Type)
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
