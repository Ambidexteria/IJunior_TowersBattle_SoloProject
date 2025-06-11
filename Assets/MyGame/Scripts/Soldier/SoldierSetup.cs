using Base.Data.Game;
using Base.Infrastructure;
using Base.Logic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Soldier
{
    public class SoldierSetup : SpawnableObject
    {
        [SerializeField] private SoldierGroundCollisionController _groundCollisionController;
        [SerializeField] private Animator _animator;
        [SerializeField] private SoldierWeapon _weapon;
        [SerializeField] private TriggerObserver _enemyTrigger;
        [SerializeField] private TriggerObserver _despawnerTrigger;
        [SerializeField] private float _dieDelay;
        [SerializeField] private List<ColorChangerMark> _marks;
        [SerializeField] private Rigidbody _rigidbody;

        private TeamColorChanger _colorChanger;
        private Team _team;
        private SoldierData _stats;
        private ICoroutineRunner _coroutineRunner;

        private SoldierModel _soldier;

        public void Init(Team team, SoldierData stats, ICoroutineRunner coroutineRunner, TeamColorChanger colorChanger)
        {
            _team = team;
            _stats = stats;
            _coroutineRunner = coroutineRunner;
            _colorChanger = colorChanger;
        }

        public SoldierModel GetSoldier()
        {
            if (_soldier == null)
            {
                _soldier = new SoldierModel(_groundCollisionController, _animator, _weapon, _enemyTrigger,
                   _despawnerTrigger, _dieDelay, _marks, _rigidbody, _team, _stats, _coroutineRunner, _colorChanger, transform);
            }

            return _soldier;
        }
    }
}
