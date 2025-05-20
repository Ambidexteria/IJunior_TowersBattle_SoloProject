using Base.Health;
using Base.Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonSetup : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _projectileCollider;
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystemController _shootEffect;
        [SerializeField] private ParticleSystemController _takeDamageEffect;
        [SerializeField] private Barrel _barrel;
        [SerializeField] private List<ColorChangerMark> _colorChangerMarks;

        private CannonModel _model;

        private CannonEnergyBarPresenter _energyBarPresenter;
        private CannonEnergyBarView _energyView;

        private HealthPresenter _healthPresenter;
        private HealthView _healthView;

        public CannonModel GetModel()
        {
            return _model;
        }

        public CannonModel CreateCannonModel(Team team, int damage,TeamColorChanger colorChanger,
            CannonProjectileSpawner projectileSpawner, CannonEnergyBar cannonEnergyBar, CannonEnergyBarView cannonEnergyBarView,
            HealthModel healthModel)
        {
            _model = new CannonModel(transform, _projectileCollider, team, _animator, _shootEffect,
                _takeDamageEffect, _barrel, damage, healthModel, projectileSpawner, colorChanger,_colorChangerMarks);

            _energyBarPresenter = new CannonEnergyBarPresenter(cannonEnergyBar, cannonEnergyBarView);
            _energyBarPresenter.Enable();

            return _model;
        }
    }
}
