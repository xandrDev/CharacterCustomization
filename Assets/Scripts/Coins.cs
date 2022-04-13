using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    private int _coinsCount;

    public event Action<int> OnUpdated;

    private void Start()
    {
        OnUpdated(_coinsCount);
    }

    public void DecreaseCoins(int value)
    {
        _coinsCount -= value;

        OnUpdated(_coinsCount);
    }

    public void IncreaseCoins(int value)
    {
        _coinsCount += value;

        OnUpdated(_coinsCount);
    }

    public bool BuyItem(Item item)
    {
        if (_coinsCount > item.Price)
        {
            DecreaseCoins(item.Price);
            return true;
        }
        else
        {
            return false;
        }
    }
}
