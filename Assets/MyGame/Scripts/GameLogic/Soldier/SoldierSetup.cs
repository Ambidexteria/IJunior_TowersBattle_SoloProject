using System;
using System.Collections.Generic;
using UnityEngine;
using Base.Data.Game;
using Base.GameLogic;
using Base.Infrastructure;
using Base.Logic;
using Base.Services.Audio;

namespace Base.Soldier
{
    public class SoldierSetup : SpawnableObject, ISelectable
    {
        [SerializeField] private SoldierView _view;
        [SerializeField] private ParticleSystemController _hitEffect;
        [SerializeField] private Transform _selectionCircle;
        [SerializeField] private SoldierGroundCollisionController _groundCollisionController;
        [SerializeField] private Animator _animator;
        [SerializeField] private SoldierWeapon _weapon;
        [SerializeField] private TriggerObserver _enemyTrigger;
        [SerializeField] private TriggerObserver _despawnerTrigger;
        [SerializeField] private float _dieDelay;
        [SerializeField] private List<ColorChangerMark> _marks;
        [SerializeField] private Rigidbody _rigidbody;

        private TeamColorChanger _colorChanger;
        private AudioPlayerService _audioPlayer;
        private Team _team;
        private SoldierData _stats;
        private ICoroutineRunner _coroutineRunner;

        private SoldierPresenter _presenter;
        private SoldierModel _soldier;

        private bool _initialized = false;

        private void OnDisable()
        {
            if (_soldier != null)
                _soldier.Disable();
        }

        public void Init(
            Team team,
            SoldierData stats,
            ICoroutineRunner coroutineRunner,
            TeamColorChanger colorChanger,
            AudioPlayerService audioPlayer)
        {
            _team = team;
            _stats = stats;
            _coroutineRunner = coroutineRunner;
            _colorChanger = colorChanger;
            _audioPlayer = audioPlayer;

            _initialized = true;
        }

        public SoldierModel GetSoldier()
        {
            if (_initialized == false)
                throw new InvalidOperationException(nameof(GetSoldier));

            if (_soldier == null)
            {
                _soldier = new SoldierModel(
                    _groundCollisionController,
                    _animator,
                    _weapon,
                    _enemyTrigger,
                   _despawnerTrigger,
                   _dieDelay,
                   _marks,
                   _rigidbody,
                   _team,
                   _stats,
                   _coroutineRunner,
                   _colorChanger,
                   transform,
                   _audioPlayer,
                   _selectionCircle,
                   _hitEffect);

                _view.Init(_stats.MaxHealth);
                _presenter = new SoldierPresenter(_soldier, _view);
                _presenter.Enable();
            }

            return _soldier;
        }
    }
}
