using Base.Logic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonSetup : MonoBehaviour
    {
        [SerializeField] private CannonEnergyBarView _energyViewPrefab;
        [SerializeField] private CannonHealthView _healthViewPrefab;
        [SerializeField] private TriggerObserver _projectileCollider;

        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystemController _shootEffect;
        [SerializeField] private ParticleSystemController _takeDamageEffect;
        [SerializeField] private Barrel _barrel;
        [SerializeField] private List<ColorChangerMark> _colorChangerMarks;

        private CannonModel _model;

        private CannonEnergyBarPresenter _energyBarPresenter;
        private CannonEnergyBarView _energyView;

        private CannonHealthPresenter _healthPresenter;
        private CannonHealthView _healthView;

        public CannonModel GetModel()
        {
            return _model;
        }

        public void Init(Team team, int damage, float maxHealth, TeamColorChanger colorChanger,
            CannonProjectileSpawner projectileSpawner, CannonEnergyBar cannonEnergyBar, CannonEnergyBarView cannonEnergyBarView,
            CannonHealthView cannonHealthView)
        {
            _model = new CannonModel(transform, _projectileCollider, team, _animator, _shootEffect,
                _takeDamageEffect, _barrel, damage, maxHealth, projectileSpawner, colorChanger, _colorChangerMarks);

            _healthPresenter = new CannonHealthPresenter(_model, cannonHealthView);

            _energyBarPresenter = new CannonEnergyBarPresenter(cannonEnergyBar, cannonEnergyBarView);
            _energyBarPresenter.Enable();
        }
    }

    public class CannonEnergyBarPresenter
    {
        private CannonEnergyBar _model;
        private CannonEnergyBarView _view;

        public CannonEnergyBarPresenter(CannonEnergyBar model, CannonEnergyBarView cannonEnergyBarView)
        {
            _model = model;
            _view = cannonEnergyBarView;
            _view.Init(_model.MaxEnergy);
        }

        public void Enable()
        {
            _model.CurrentEnergyChanged += OnCurrentEnergyChanged;
        }

        public void Disable()
        {
            _model.CurrentEnergyChanged -= OnCurrentEnergyChanged;
        }

        private void OnCurrentEnergyChanged(float amount)
        {
            _view.Display(amount);
        }
    }
}
