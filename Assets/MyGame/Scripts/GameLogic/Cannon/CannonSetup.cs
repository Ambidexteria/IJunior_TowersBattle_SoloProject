using System.Collections.Generic;
using Base.Health;
using Base.Logic;
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

        public CannonModel CreateCannonModel(
            Team team,
            int damage,
            TeamColorChanger colorChanger,
            CannonProjectileSpawner projectileSpawner,
            HealthModel healthModel)
        {
            _model = new CannonModel(
                transform,
                _projectileCollider,
                team,
                _animator,
                _shootEffect,
                _takeDamageEffect,
                _barrel,
                damage,
                healthModel,
                projectileSpawner,
                colorChanger,
                _colorChangerMarks);

            return _model;
        }
    }
}
