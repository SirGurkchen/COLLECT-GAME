using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _player;

    private CoinLogic[] coins;
    private List<CoinLogic> _coinsList;
    private int collectedCoins = 0;

    private void Start()
    {
        _coinsList = new List<CoinLogic>();

        coins = FindObjectsByType<CoinLogic>(FindObjectsSortMode.None);
        foreach (var coin in coins)
        {
            _coinsList.Add(coin);
            coin.OnCoinCollect += Coin_OnCoinCollect;
        }
    }

    private void Coin_OnCoinCollect()
    {
        collectedCoins++;
        _uiManager.RefreshCollectedCoins(collectedCoins);
        _audioManager.PlaySound(AudioManager.SoundType.Collect);
    }

    public void AddCoin(CoinLogic coin)
    {
        _coinsList.Add(coin);
        coin.OnCoinCollect += Coin_OnCoinCollect;
    }
}