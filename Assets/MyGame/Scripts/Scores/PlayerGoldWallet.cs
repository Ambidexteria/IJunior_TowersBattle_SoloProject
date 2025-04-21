using System;
using UnityEngine;

public class PlayerGoldWallet : MonoBehaviour
{
    [SerializeField] private int _goldCount;

    public int GoldCount => _goldCount;

    public void Add(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException();

        _goldCount += amount;
    }

    public bool TryTake(int amount)
    {
        if (_goldCount >= amount)
        {
            _goldCount -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
