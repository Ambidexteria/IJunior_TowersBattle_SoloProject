using System;
using UnityEngine;
using Zenject;

namespace Base.GameLogic.Cannon
{
    [RequireComponent(typeof(Collider))]
    public class CannonProjectile : SpawnableObject
    {
        [SerializeField] private ColorChangerMark _markForRecoloring;
        [SerializeField] private int _damage;
        [SerializeField] private int _speed;
        [SerializeField] private PathFollower _follower;
        [SerializeField] private TeamColorChanger _colorChanger;

        private Collider _collider;
        private Team _team;

        public TeamType TeamType => _team.Type;
        public int Damage => _damage;

        public event Action<CannonProjectile> Despawning;

        [Inject]
        private void Init(TeamColorChanger colorChanger)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonProjectile), nameof(Init), colorChanger);

            _colorChanger = colorChanger;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();

            ExceptionsTest.NullRefMethodTest(nameof(CannonProjectile), nameof(Awake), _markForRecoloring, _follower, _colorChanger);

            _collider.enabled = false;
        }

        private void OnDisable()
        {
            _collider.enabled = false;
        }

        public void Init(Team team, Vector3 start, Vector3 fifnish, int damage)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonProjectile), nameof(Init), team);

            _team = team;
            _colorChanger.Recolor(_team, _markForRecoloring);
            _damage = damage;
            _follower.StartMovement(_speed, start, fifnish);
            _collider.enabled = true;
        }

        public void Despawn()
        {
            Despawning?.Invoke(this);
        }
    }
}
