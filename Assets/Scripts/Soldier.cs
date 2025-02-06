using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Soldier : SpawnableObject, IDamageable
{
    [SerializeField] private SoldierMoverToTarget _moverToTarget;
    [SerializeField]private Animator _animator;

    public Animator Animator => _animator;
    public SoldierMoverToTarget MoverToTarget => _moverToTarget;

    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }
    public bool IsDead()
    {
        return false;
    }
}
