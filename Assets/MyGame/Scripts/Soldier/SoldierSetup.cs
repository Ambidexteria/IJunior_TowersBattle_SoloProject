using Base.Data.Game;
using Base.GameLogic;
using Base.Infrastructure;
using Base.Logic;
using Base.Services.Audio;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Soldier
{
    public class SoldierSetup : SpawnableObject, ISelectable
    {
        [SerializeField] private SoldierView _view;
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

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(SoldierSetup), nameof(Awake),
                _groundCollisionController, _animator, _weapon, _enemyTrigger, _despawnerTrigger, _marks, _rigidbody);
            ExceptionsTest.EmptyListTest(nameof(SoldierSetup), nameof(Awake), _marks);
        }

        private void OnDisable()
        {
            if (_soldier != null)
                _soldier.Disable();
        }

        public void Init(Team team, SoldierData stats, ICoroutineRunner coroutineRunner, TeamColorChanger colorChanger,
            AudioPlayerService audioPlayer)
        {
            ExceptionsTest.NullRefMethodTest(nameof(SoldierSetup), nameof(Init),
                team, stats, coroutineRunner, colorChanger);

            _team = team;
            _stats = stats;
            _coroutineRunner = coroutineRunner;
            _colorChanger = colorChanger;
            _audioPlayer = audioPlayer;
        }

        public SoldierModel GetSoldier()
        {
            if (_soldier == null)
            {
                _soldier = new SoldierModel(_groundCollisionController, _animator, _weapon, _enemyTrigger,
                   _despawnerTrigger, _dieDelay, _marks, _rigidbody, _team, _stats, _coroutineRunner, _colorChanger, 
                   transform, _audioPlayer, _selectionCircle);

                _view.Init(_stats.MaxHealth);
                _presenter = new(_soldier, _view);
                _presenter.Enable();
            }

            return _soldier;
        }
    }
}
