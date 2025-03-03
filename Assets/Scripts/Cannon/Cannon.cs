using SplineMesh;
using UnityEngine;

public class Cannon : MonoBehaviour, IDamageable
{
    [SerializeField] private Cannon _enemyCannon;
    [SerializeField] private Health _health;
    [SerializeField] private CannonEnergyBar _energyBar;
    [SerializeField] private Transform _barrel;
    [SerializeField] private CannonProjectile _projectilePrefab;
    [SerializeField] private float _damage; 
    [SerializeField] private float _fireDelay;

    [SerializeField] private Spline _spline;

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

    public void Shoot()
    {
        CannonProjectile cannonProjectile = Instantiate(_projectilePrefab, _barrel.position, Quaternion.identity);

    }

    public void TakeDamage(int amount)
    {
        _health.Decrease(amount);
    }
}
