using Base.Logic;
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
        private CannonEnergyBarView _energyView;

        private CannonPresenter _presenter;
        private CannonHealthView _healthView;

        public CannonModel GetModel()
        {
            return _model;
        }

        public void Init(Team team, int damage, float fireDelay)
        {
            _model = new CannonModel(transform, _projectileCollider, team, _animator, _shootEffect,
                _takeDamageEffect, _barrel, damage, fireDelay, _colorChangerMarks);

            //_energyViewPrefab = Instantiate(_energyViewPrefab);
            _healthView = Instantiate(_healthViewPrefab);

            _presenter = new CannonPresenter(_model, _healthView);
        }
    }
}
