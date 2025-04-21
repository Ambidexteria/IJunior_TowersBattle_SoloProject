using UnityEngine;

[System.Serializable]
public class SoldierStats
{
    [Header("General")]
    [SerializeField] private float _maxHealth;

    [Header("Moving")]
    [SerializeField] private float _minDistanceToTarget = 2f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _brakeSpeed = 2f;

    [Header("Weapon")]
    [SerializeField] private int _weaponDamage;

    public float MaxHealth => _maxHealth;
    public int WeaponDamage => _weaponDamage;

    public float MinDistanceToTarget => _minDistanceToTarget;
    public float Speed => _speed;
    public float BrakeSpeed => _brakeSpeed;
}
