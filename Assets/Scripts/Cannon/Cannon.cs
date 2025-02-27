using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour, IDamageable
{
    [SerializeField] private Cannon _enemyCannon;
    [SerializeField] private CannonEnergyBar _energyBar;
    [SerializeField] private float _damage;
    [SerializeField] private float _fireDelay;

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
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        Debug.Log("Cannon is shooting!");
    }

    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }
}
