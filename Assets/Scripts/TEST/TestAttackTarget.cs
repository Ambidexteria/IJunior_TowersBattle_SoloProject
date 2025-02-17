using UnityEngine;

public class TestAttackTarget : MonoBehaviour, IDamageable
{
    public bool IsDead()
    {
        return false;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"{amount} damage taken");
    }
}
