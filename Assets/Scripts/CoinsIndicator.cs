using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinsIndicator : MonoBehaviour
{
    [SerializeField]
    private Coins _coins = null;

    public Text CoinsTxt;

    private void Start()
    {
        if (_coins == null)
            throw new Exception("Coins not defined");
    }

    private void OnEnable()
    {
        _coins.OnUpdated += Coins_OnUpdated;
    }

    private void OnDisable()
    {
        _coins.OnUpdated -= Coins_OnUpdated;
    }

    private void Coins_OnUpdated(int coins)
    {
        CoinsTxt.text = $"Total coins: {coins}";
    }
}
