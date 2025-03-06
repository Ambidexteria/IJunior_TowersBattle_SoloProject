using UnityEngine;

public class Cannon : MonoBehaviour, IDamageable
{
    [SerializeField] private TeamType _team;
    [SerializeField] private Cannon _enemyCannon;
    [SerializeField] private Health _health;
    [SerializeField] private CannonEnergyBar _energyBar;
    [SerializeField] private Barrel _barrel;
    [SerializeField] private CannonProjectile _projectilePrefab;
    [SerializeField] private int _damage; 
    [SerializeField] private float _fireDelay;

    public TeamType Team => _team;

    public Vector3 ShootDirection => _barrel.ShootDirection;

    private void OnEnable()
    {
        _energyBar.Filled += Shoot;
    }

    private void OnDisable()
    {
        _energyBar.Filled -= Shoot;
    }

    public bool IsDead()
    {
        return _health.IsDead;
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        CannonProjectile cannonProjectile = Instantiate(_projectilePrefab, _barrel.StartPoint, Quaternion.identity);
        cannonProjectile.Init(Team, _barrel.StartPoint, _enemyCannon.transform.position, _damage);
    }

    public void TakeDamage(int amount)
    {
        _health.Decrease(amount);
    }
}
