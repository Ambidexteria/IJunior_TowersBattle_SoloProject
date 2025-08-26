using System;
using UnityEngine;
using Zenject;

namespace Base.GameLogic.Cannon
{
    [RequireComponent(typeof(Collider))]
    public class CannonProjectile : SpawnableObject
    {
        [SerializeField] private int _speed = 5;
        [SerializeField] private Collider _collider;
        [SerializeField] private ColorChangerMark _markForRecoloring;
        [SerializeField] private PathFollower _follower;
        [SerializeField] private TeamColorChanger _colorChanger;

        private int _damage;
        private Team _team;

        public event Action<CannonProjectile> Despawning;

        public TeamType TeamType => _team.Type;
        public int Damage => _damage;

        [Inject]
        private void Init(TeamColorChanger colorChanger)
        {
            _colorChanger = colorChanger;
        }

        private void Awake()
        {
            _collider.enabled = false;
        }

        public void Init(Team team, Vector3 start, Vector3 fifnish, int damage)
        {
            _team = team;
            _colorChanger.Recolor(_team, _markForRecoloring);
            _damage = damage;
            _follower.StartMovement(_speed, start, fifnish);
            _collider.enabled = true;
        }

        public void Despawn()
        {
            Despawning?.Invoke(this);
            _collider.enabled = false;
        }
    }
}
